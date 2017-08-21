using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    protected float jumpStartTime;

    public override bool ExecuteInput()
    {
        if(Time.time - jumpStartTime > player.jumpDuration)
        {
            machine.ChangeState<StandingState>();
            return true;
        }

        return false;
    }

    public override void ExecuteFrame()
    {
        float newY;
        float jumpTime = Time.time - jumpStartTime;
        float jumpHeight = GetJumpHeight(jumpTime);
        if (jumpTime >= player.jumpDuration)
        {
            newY = player.groundHeight;
        }
        else
        {
            newY = player.groundHeight + jumpHeight;
        }
        player.transform.localPosition = new Vector2(player.transform.localPosition.x, newY);
    }

    public override void EnterState()
    {
        jumpStartTime = Time.time;
    }

    protected float GetJumpHeight(float time)
    {
        float a = 10f;
        float diff = time - 0.5f;
        float height = (-a * diff * diff) + 3f;
        return Mathf.Clamp(height, 0, 100);
    }

}
