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

    [Header("State Variables")]
    public int currHp;
    public int currMeter;
    public bool isFacingRight;
    public bool isJumping;

    public GameObject hpBar;
    private Rigidbody2D body;
    [SerializeField]
    private PlayerState state;
    private Queue<PlayerInputFrame> inputBuffer = new Queue<PlayerInputFrame>(30); //TODO: Make this queue not delete inputs if performance becomes an issue
    private PlayerInputFrame currentInputFrame; // Convenience var to track last input
    private float jumpStartTime;
    private float jumpDuration = 1f;
    private float groundHeight;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        GameManager.instance.RegisterPlayer(this);
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.STANDING;
        currHp = maxHp;
        currMeter = maxMeter;
        groundHeight = transform.localPosition.y;
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
        UpdatePosition(); // Move player based on state if needed
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

    private void UpdateInputBuffer()
    {
        PlayerInputFrame inputFrame = new PlayerInputFrame();

        if (Input.GetAxisRaw("Vertical" + id) > 0)
            inputFrame.AddToFrame(PlayerInputButton.UP);
        if (Input.GetAxisRaw("Vertical" + id) < 0)
            inputFrame.AddToFrame(PlayerInputButton.DOWN);
        if (Input.GetAxisRaw("Horizontal" + id) < 0)
            inputFrame.AddToFrame(PlayerInputButton.LEFT);
        if (Input.GetAxisRaw("Horizontal" + id) > 0)
            inputFrame.AddToFrame(PlayerInputButton.RIGHT);
        if (Input.GetButton("A" + id))
            inputFrame.AddToFrame(PlayerInputButton.A);
        if (Input.GetButton("B" + id))
            inputFrame.AddToFrame(PlayerInputButton.B);
        if (Input.GetButton("C" + id))
            inputFrame.AddToFrame(PlayerInputButton.C);
        if (Input.GetButton("D" + id))
            inputFrame.AddToFrame(PlayerInputButton.D);
        if (Input.GetButton("Swap" + id))
            inputFrame.AddToFrame(PlayerInputButton.SWAP);

        inputBuffer.Enqueue(inputFrame);
        currentInputFrame = inputFrame;
    }

    /// <summary>
    /// TODO: Replace with mecanim FSM
    /// </summary>
    private void UpdateState()
    {
        if (isJumping)
        {
            return;
        }

        if (currentInputFrame.IsButtonPressed(PlayerInputButton.UP) && currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
        {
            isJumping = true;
            jumpStartTime = Time.time;

            if (currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
            {
                if (isFacingRight)
                    state = PlayerState.JUMPING_FORWARD;
                else
                    state = PlayerState.JUMPING_BACKWARD;
            }
            else if (currentInputFrame.IsButtonPressed(PlayerInputButton.LEFT))
            {
                if (isFacingRight)
                    state = PlayerState.JUMPING_BACKWARD;
                else
                    state = PlayerState.JUMPING_FORWARD;
            }
            else
                state = PlayerState.JUMPING_UP;
        }
        else if (currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
        {
            if (isFacingRight)
                state = PlayerState.WALKING_FORWARD;
            else
                state = PlayerState.WALKING_BACKWARD;
        }
        else if (currentInputFrame.IsButtonPressed(PlayerInputButton.LEFT))
        {
            if (isFacingRight)
                state = PlayerState.WALKING_BACKWARD;
            else
                state = PlayerState.WALKING_FORWARD;
        }
        else
        {
            state = PlayerState.STANDING;
        }
    }

    private void UpdatePosition()
    {
        switch (state)
        {
            case PlayerState.STANDING:
                break;
            case PlayerState.BLOCKING:
                break;
            case PlayerState.WALKING_BACKWARD:
                {
                    float delta = (isFacingRight ? -1 : 1) * walkSpeed;
                    transform.position += new Vector3(delta, 0);
                }
                break;
            case PlayerState.WALKING_FORWARD:
                {
                    float delta = (isFacingRight ? 1 : -1) * walkSpeed;
                    transform.position += new Vector3(delta, 0);
                }
                break;
            case PlayerState.DASHING_BACK:
                break;
            case PlayerState.DASHING_FORWARD:
                break;
            case PlayerState.JUMPING_UP:
                {
                    float newX = transform.localPosition.x;
                    float newY;
                    float jumpTime = Time.time - jumpStartTime;
                    float jumpHeight = GetJumpHeight(jumpTime);
                    if (jumpTime >= jumpDuration)
                    {
                        isJumping = false;
                        newY = groundHeight;
                    }
                    else
                    {
                        newY = groundHeight + jumpHeight;
                    }
                    transform.localPosition = new Vector2(newX, newY);
                }
                break;
            case PlayerState.JUMPING_BACKWARD:
                {
                    float newX = transform.localPosition.x + (isFacingRight ? -1 : 1) * walkSpeed;
                    float newY;
                    float jumpTime = Time.time - jumpStartTime;
                    float jumpHeight = GetJumpHeight(jumpTime);
                    if (jumpTime >= jumpDuration)
                    {
                        isJumping = false;
                        newY = groundHeight;
                    }
                    else
                    {
                        newY = groundHeight + jumpHeight;
                    }
                    transform.localPosition = new Vector2(newX, newY);
                }
                break;
            case PlayerState.JUMPING_FORWARD:
                {
                    float newX = transform.localPosition.x + (isFacingRight ? 1 : -1) * walkSpeed;
                    float newY;
                    float jumpTime = Time.time - jumpStartTime;
                    float jumpHeight = GetJumpHeight(jumpTime);
                    if (jumpTime >= jumpDuration)
                    {
                        isJumping = false;
                        newY = groundHeight;
                    }
                    else
                    {
                        newY = groundHeight + jumpHeight;
                    }
                    transform.localPosition = new Vector2(newX, newY);
                }
                break;
            case PlayerState.DASH_JUMPING_BACK:
                break;
            case PlayerState.DASH_JUMPING_FORWARD:
                break;
            case PlayerState.AIRDASHING_BACK:
                break;
            case PlayerState.AIRDASHING_FORWARD:
                break;
            case PlayerState.ATTACKING:
                break;
            default:
                break;
        }
    }

    private float GetJumpHeight(float time)
    {
        float a = 10f;
        float diff = time - 0.5f;
        float height = (-a * diff * diff) + 3f;
        return Mathf.Clamp(height, 0, 100);
    }
}
