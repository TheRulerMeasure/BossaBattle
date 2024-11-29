using Godot;
using System;

public class BaseState : Resource
{
    public virtual void Enter()
    {

    }

    public virtual string Tick(float delta)
    {
        return string.Empty;
    }

    public virtual string TickPhysics(float delta)
    {
        return string.Empty;
    }

    public virtual void Exit()
    {

    }
}
