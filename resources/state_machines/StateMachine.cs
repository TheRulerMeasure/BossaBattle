using Godot;
using Godot.Collections;
using System;

public class StateMachine : Resource
{
    private readonly Dictionary<string, BaseState> _states = new Dictionary<string, BaseState>();

    private BaseState _currentState;
    private string _previousKey = string.Empty;

    public void InsertState(string key, BaseState state)
    {
        _states.Add(key, state);
    }

    public void MakeStateInitial(string key)
    {
        _currentState = _states[key];
        _currentState.Enter(_previousKey);
        _previousKey = key;
    }

    public void Tick(float delta)
    {
        string key = _currentState.Tick(delta);
        if (!string.IsNullOrEmpty(key))
        {
            ChangeState(_states[key], _previousKey);
            _previousKey = key;
        }
    }

    public void TickPhysics(float delta)
    {
        string key = _currentState.TickPhysics(delta);
        if (!string.IsNullOrEmpty(key))
        {
            ChangeState(_states[key], _previousKey);
            _previousKey = key;
        }
    }

    private void ChangeState(BaseState newState, string prevKey)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter(prevKey);
    }
}
