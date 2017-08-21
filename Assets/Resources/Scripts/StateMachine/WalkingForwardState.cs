using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingForwardState : PlayerState
{

    public override void ExecuteInput()
    {
        if (!player.currentInputFrame.IsForwardPressed())
        {
            machine.ChangeState<StandingState>();
        }
    }

    public override void ExecuteFrame()
    {
        float delta = (player.isFacingRight ? 1 : -1) * player.walkSpeed;
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
