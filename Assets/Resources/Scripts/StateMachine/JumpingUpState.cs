using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingUpState : PlayerState
{
    private float jumpStartTime;

    public override void ExecuteInput()
    {
        Debug.Log("Input " + this.GetType() + " State");
    }

    public override void ExecuteFrame()
    {
        Debug.Log("Update" + this.GetType() + " State");
    }

    public override void EnterState()
    {
        jumpStartTime = Time.time;

        Debug.Log("Enter" + this.GetType() + " State");
    }

    public override void ExitState()
    {
        Debug.Log("Exit" + this.GetType() + " State");
    }

}
