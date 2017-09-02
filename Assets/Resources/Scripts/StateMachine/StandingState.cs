using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : PlayerState
{

    public override bool ExecuteInput()
    {
        PlayerInputFrame currentInputFrame = player.currentInputFrame;
        InputBuffer inputBuffer = player.inputBuffer;

        if (currentInputFrame.IsButtonPressed(PlayerInputButton.UP))
        {
            if (currentInputFrame.IsForwardPressed())
            {
                machine.ChangeState<JumpingForwardState>();
            }
            else if (currentInputFrame.IsBackwardPressed())
            {
                machine.ChangeState<JumpingBackwardState>();
            }
            else
                machine.ChangeState<JumpingUpState>();
        }
        else if (currentInputFrame.IsForwardPressed())
        {
            machine.ChangeState<WalkingForwardState>();
        }
        else if (currentInputFrame.IsBackwardPressed())
        {
            machine.ChangeState<WalkingBackwardState>();
        }
        else if(inputBuffer.IsButtonPressed(PlayerInputButton.A))
        {
            machine.ChangeState<StandingAState>();
        }
        else
        {
            machine.ChangeState<StandingState>();
        }

        return false;
    }

    public override void ExecuteFrame()
    {

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
