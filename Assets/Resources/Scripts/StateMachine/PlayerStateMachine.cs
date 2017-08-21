using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;
    [HideInInspector]
    public PlayerController player;

    private Dictionary<Type, PlayerState> states = new Dictionary<Type, PlayerState>();

    void Start()
    {
        player = GetComponent<PlayerController>();
        AddStates();
    }

    public void UpdateFrame()
    {
        currentState.ExecuteInput();
        currentState.ExecuteFrame();
    }

    public void ChangeState<T>() where T : PlayerState
    {
        PlayerState nextState = states[typeof(T)];
        if(currentState != nextState)
        {
            currentState.ExitState();
            currentState = nextState;
            nextState.EnterState();
            UpdateFrame();
        }
        //TODO: Check states against other players
    }

    public bool IsCurrentState<T>() where T : PlayerState
    {
        return currentState.GetType() == typeof(T);
    }

    private void AddStates()
    {
        AddState<StandingState>();
        AddState<StandingBlockState>();
        AddState<StandingAState>();
        AddState<StandingBState>();
        AddState<StandingCState>();
        AddState<StandingDState>();

        AddState<CrouchingState>();
        AddState<CrouchingBlockState>();
        AddState<CrouchingAState>();
        AddState<CrouchingBState>();
        AddState<CrouchingCState>();
        AddState<CrouchingDState>();

        AddState<WalkingForwardState>();
        AddState<WalkingBackwardState>();
        AddState<DashingForwardState>();
        AddState<DashingBackwardState>();

        AddState<JumpingUpState>();
        AddState<JumpingBackwardState>();
        AddState<JumpingForwardState>();
        AddState<DashJumpingBackwardState>();
        AddState<DashJumpingForwardState>();
        AddState<JumpingAState>();
        AddState<JumpingBState>();
        AddState<JumpingCState>();
        AddState<JumpingDState>();

        foreach (KeyValuePair<Type, PlayerState> state in states)
        {
            state.Value.Initialize();
        }

        currentState = states[typeof(StandingState)];
    }

    private void AddState<T>() where T : PlayerState, new()
    {
        if (!states.ContainsKey(typeof(T)))
        {
            PlayerState item = new T();
            item.machine = this;
            states.Add(typeof(T), item);
        }
    }
}
