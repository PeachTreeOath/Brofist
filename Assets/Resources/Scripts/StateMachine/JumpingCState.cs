using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCState : JumpingState
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
