using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    private float jumpStartTime;
    private float jumpDuration = 1f;
    private float groundHeight;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.RegisterPlayer(this);
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.STANDING;
        currHp = maxHp;
        currMeter = maxMeter;
        groundHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Swap" + id))
        {
            GameManager.instance.Swap(this);
        }
        UpdateState();
        UpdatePosition();
    }

    public void SetHpBar()
    {
        float missingHp = (float)currHp / (float)maxHp;
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

    /// <summary>
    /// TODO: Replace with mecanim FSM
    /// </summary>
    private void UpdateState()
    {
        if (isJumping)
        {
            return;
        }

        if (Input.GetAxisRaw("Vertical" + id) > 0 && Input.GetAxisRaw("Horizontal" + id) > 0)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            if (isFacingRight)
                state = PlayerState.JUMPING_FORWARD;
            else
                state = PlayerState.JUMPING_BACKWARD;
        }
        else if (Input.GetAxisRaw("Vertical" + id) > 0 && Input.GetAxisRaw("Horizontal" + id) < 0)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            if (isFacingRight)
                state = PlayerState.JUMPING_BACKWARD;
            else
                state = PlayerState.JUMPING_FORWARD;
        }
        else if (Input.GetAxisRaw("Vertical" + id) > 0)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            state = PlayerState.JUMPING_UP;
        }
        else if (Input.GetAxisRaw("Horizontal" + id) > 0)
        {
            if (isFacingRight)
                state = PlayerState.WALKING_FORWARD;
            else
                state = PlayerState.WALKING_BACKWARD;
        }
        else if (Input.GetAxisRaw("Horizontal" + id) < 0)
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
                    float delta = (isFacingRight ? -1 : 1) * walkSpeed * Time.deltaTime;
                    transform.position += new Vector3(delta, 0);
                }
                break;
            case PlayerState.WALKING_FORWARD:
                {
                    float delta = (isFacingRight ? 1 : -1) * walkSpeed * Time.deltaTime;
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
                    float newX = transform.localPosition.x + (isFacingRight ? -1 : 1) * walkSpeed * Time.deltaTime;
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
                    float newX = transform.localPosition.x + (isFacingRight ? 1 : -1) * walkSpeed * Time.deltaTime;
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
