using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCState : PlayerState
{

    public override bool ExecuteInput()
    {
        Debug.Log("Input " + this.GetType() + " State"); return false;

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
