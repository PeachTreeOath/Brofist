using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBackwardState : PlayerState
{

    public override void ExecuteInput()
    {
        if (!player.currentInputFrame.IsBackwardPressed())
        {
            machine.ChangeState<StandingState>();
        }
    }

    public override void ExecuteFrame()
    {
        Debug.Log("Update" + this.GetType() + " State");
    }

    public override void EnterState()
    {
        Debug.Log("Enter" + this.GetType() + " State");
    }

    public override void ExitState()
    {
        Debug.Log("Exit" + this.GetType() + " State");
    }

}
