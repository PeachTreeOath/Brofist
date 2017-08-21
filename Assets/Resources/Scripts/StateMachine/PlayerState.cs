using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{

    public PlayerStateMachine machine;

    protected PlayerController player;

    /// <summary>
    /// Called when machine is created
    /// </summary>
    public virtual void Initialize()
    {
        player = machine.player;
    }

    /// <summary>
    /// Called every frame tick, checks for input to transition state
    /// </summary>
    /// <returns>Returns true is state needed to be changed. 
    /// This is used for derived state classes to stop execution early.
    /// If a state has no derived classes then just return false.</returns>
    public virtual bool ExecuteInput() { return false; }

    /// <summary>
    /// Called every frame tick, executes actual game logic
    /// </summary>
    public virtual void ExecuteFrame() { }

    /// <summary>
    /// Called when state transitions to this state
    /// </summary>
    public virtual void EnterState() { }

    /// <summary>
    /// Called when transitioning away from this state
    /// </summary>
    public virtual void ExitState() { }
}
