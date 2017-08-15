using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public bool isFacingRight;

    private Rigidbody2D body;
    private PlayerState state;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.STANDING;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdatePosition();
    }

    private void UpdateState()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (isFacingRight)
                state = PlayerState.WALKING_FORWARD;
            else
                state = PlayerState.WALKING_BACKWARD;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
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
                    float delta = (isFacingRight ? -1 : 1) * speed * Time.deltaTime;
                    transform.position += new Vector3(delta, 0);
                }
                break;
            case PlayerState.WALKING_FORWARD:
                {
                    float delta = (isFacingRight ? 1 : -1) * speed * Time.deltaTime;
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
