using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingUpState : JumpingState
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
