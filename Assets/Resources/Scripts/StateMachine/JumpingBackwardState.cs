using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBackwardState : JumpingState
{

    public override bool ExecuteInput()
    {
        if (base.ExecuteInput())
        {
            return true;
        }

        return false;
    }

    public override void ExecuteFrame()
    {
        base.ExecuteFrame();

        float delta = (player.isFacingRight ? -1 : 1) * player.walkSpeed;
        player.transform.position += new Vector3(delta, 0);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        Debug.Log("Exit" + this.GetType() + " State");
    }

}
