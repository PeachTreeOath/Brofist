using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : FrameDependentEntity
{
    [Header("Character Stats")]
    public int id; // Players 1 and 2 are on same team, 3 and 4 on the other
    public float walkSpeed;
    public float runSpeed;
    public float airdashSpeed;
    public int maxHp = 1000;
    public int maxMeter = 100;
    public float jumpDuration = 1f;
    public float groundHeight;

    [Header("Move List")]
    public AttackData attack5A;

    [Header("Debug State Variables")]
    public int currHp;
    public int currMeter;
    public bool isFacingRight;
    public bool isJumping;
    [HideInInspector]
    public PlayerInputFrame currentInputFrame; // Convenience var to track last input

    public GameObject hpBar;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerStateMachine fsm;
    public InputBuffer inputBuffer; //TODO: Consolidate PlayerInputFrame api into input buffer

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        GameManager.instance.RegisterPlayer(this);
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fsm = gameObject.AddComponent<PlayerStateMachine>();
        currHp = maxHp;
        currMeter = maxMeter;
        groundHeight = transform.localPosition.y;
        inputBuffer = new InputBuffer();
        currentInputFrame = new PlayerInputFrame(isFacingRight);
    }

    void Update()
    {
        UpdateInputFrame();
    }

    // Update is called once per frame
    public override void UpdateFrame()
    {
        if (Input.GetButtonDown("Swap" + id))
        {
            GameManager.instance.Swap(this);
        }
        UpdateInputBuffer(); // Grab all inputs from player this frame and queue input buffer
        UpdateState(); // Change player state based on inputs
        currentInputFrame = new PlayerInputFrame(isFacingRight); // Create new input buffer for next frame
    }

    public void SetHpBar()
    {
        float missingHp = currHp / maxHp;
        hpBar.transform.localScale = new Vector3(missingHp, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }

    public void TakeDamage(int damage)
    {
        currHp -= damage;
        if (currHp < 0)
            currHp = 0;
        SetHpBar();
    }

    public void UseMeter(int meter)
    {
        currMeter -= meter;
        if (currMeter < 0)
            currMeter = 0;
    }

    public void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    /// <summary>
    /// Save off player inputs in between frames for input leniency
    /// </summary>
    private void UpdateInputFrame()
    {
        if (Input.GetAxisRaw("Vertical" + id) > 0)
            currentInputFrame.AddToFrame(PlayerInputButton.UP);
        if (Input.GetAxisRaw("Vertical" + id) < 0)
            currentInputFrame.AddToFrame(PlayerInputButton.DOWN);
        if (Input.GetAxisRaw("Horizontal" + id) < 0)
            currentInputFrame.AddToFrame(PlayerInputButton.LEFT);
        if (Input.GetAxisRaw("Horizontal" + id) > 0)
            currentInputFrame.AddToFrame(PlayerInputButton.RIGHT);
        if (Input.GetButton("A" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.A);
        if (Input.GetButton("B" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.B);
        if (Input.GetButton("C" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.C);
        if (Input.GetButton("D" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.D);
        if (Input.GetButton("Swap" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.SWAP);
        if (Input.GetButtonUp("A" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.A_RELEASE);
        if (Input.GetButtonUp("B" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.B_RELEASE);
        if (Input.GetButtonUp("C" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.C_RELEASE);
        if (Input.GetButtonUp("D" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.D_RELEASE);
        if (Input.GetButtonUp("Swap" + id))
            currentInputFrame.AddToFrame(PlayerInputButton.SWAP_RELEASE);
    }

    /// <summary>
    /// Update the input buffer itself when the frame clock ticks
    /// </summary>
    private void UpdateInputBuffer()
    {
        inputBuffer.Enqueue(currentInputFrame);
    }

    /// <summary>
    /// TODO: Replace with mecanim FSM
    /// </summary>
    private void UpdateState()
    {
        fsm.UpdateFrame();
    }

}
