using Godot;
using Godot.Collections;
using System;

public class StateMachine : Resource
{
    private readonly Dictionary<string, BaseState> _states = new Dictionary<string, BaseState>();

    private BaseState _currentState;

    public void InsertState(string key, BaseState state)
    {
        _states.Add(key, state);
    }

    public void MakeStateInitial(string key)
    {
        _currentState = _states[key];
        _currentState.Enter();
    }

    public void Tick(float delta)
    {
        string key = _currentState.Tick(delta);
        if (!string.IsNullOrEmpty(key))
        {
            ChangeState(_states[key]);
        }
    }

    public void TickPhysics(float delta)
    {
        string key = _currentState.TickPhysics(delta);
        if (!string.IsNullOrEmpty(key))
        {
            ChangeState(_states[key]);
        }
    }

    private void ChangeState(BaseState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
