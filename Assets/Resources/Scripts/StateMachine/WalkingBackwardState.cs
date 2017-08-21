using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBackwardState : PlayerState
{

    public override bool ExecuteInput()
    {
        if (!player.currentInputFrame.IsOnlyBackwardPressed())
        {
            machine.ChangeState<StandingState>();
        }

        return false;
    }

    public override void ExecuteFrame()
    {
        float delta = (player.isFacingRight ? -1 : 1) * player.walkSpeed;
        player.transform.position += new Vector3(delta, 0);
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
