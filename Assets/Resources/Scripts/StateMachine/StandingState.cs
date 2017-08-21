using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : PlayerState
{

    public override void ExecuteInput()
    {
        PlayerInputFrame currentInputFrame = player.currentInputFrame;
        bool isFacingRight = player.isFacingRight;

        if (currentInputFrame.IsButtonPressed(PlayerInputButton.UP) && currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
        {
            if (currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
            {
                if (isFacingRight)
                    machine.ChangeState<JumpingForwardState>();
                else
                    machine.ChangeState<JumpingBackwardState>();
            }
            else if (currentInputFrame.IsButtonPressed(PlayerInputButton.LEFT))
            {
                if (isFacingRight)
                    machine.ChangeState<JumpingBackwardState>();
                else
                    machine.ChangeState<JumpingForwardState>();
            }
            else
                machine.ChangeState<JumpingUpState>();
        }
        else if (currentInputFrame.IsButtonPressed(PlayerInputButton.RIGHT))
        {
            if (isFacingRight)
                machine.ChangeState<WalkingForwardState>();
            else
                machine.ChangeState<WalkingBackwardState>();
        }
        else if (currentInputFrame.IsButtonPressed(PlayerInputButton.LEFT))
        {
            if (isFacingRight)
                machine.ChangeState<WalkingBackwardState>();
            else
                machine.ChangeState<WalkingForwardState>();
        }
        else
        {
            machine.ChangeState<StandingState>();
        }
    }

    public override void ExecuteFrame()
    {
      //  Debug.Log("Update Idle State");
    }

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle State");
    }

}
