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

    private Rigidbody2D body;
    private PlayerState state;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.RegisterPlayer(this);
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.STANDING;
        currHp = maxHp;
        currMeter = maxMeter;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.instance.Swap(this);
        }
        UpdateState();
        UpdatePosition();
    }

    public void TakeDamage(int damage)
    {
        currHp -= damage;
        if (currHp < 0)
            currHp = 0;
    }

    public void UseMeter(int meter)
    {
        currMeter -= meter;
        if (currMeter < 0)
            currMeter = 0;
    }

    private void UpdateState()
    {
        if (Input.GetAxisRaw("Horizontal" + id) > 0)
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
                break;
            case PlayerState.JUMPING_BACK:
                break;
            case PlayerState.JUMPING_FORWARD:
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
}
