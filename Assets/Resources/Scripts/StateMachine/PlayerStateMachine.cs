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
        AddState<WalkingForwardState>();

        foreach(KeyValuePair<Type, PlayerState> state in states)
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
