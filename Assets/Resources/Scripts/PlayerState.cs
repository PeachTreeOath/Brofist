using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    STANDING,
    CROUCHING,
    BLOCKING,
    CROUCH_BLOCKING,
    WALKING_BACKWARD,
    WALKING_FORWARD,
    DASHING_BACK,
    DASHING_FORWARD,
    JUMPING_UP,
    JUMPING_BACK,
    JUMPING_FORWARD,
    DASH_JUMPING_BACK,
    DASH_JUMPING_FORWARD,
    AIRDASHING_BACK,
    AIRDASHING_FORWARD,
    ATTACKING // This will probably change to a bunch of different attacks
}
