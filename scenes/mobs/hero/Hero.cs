using BossaBattle.resources.states.hero;
using Godot;
using System;

public class Hero : MobBody
{
    private ResHero _res = new ResHero();

    private StateMachine _stateMachine = new StateMachine();

    public override void _Ready()
    {
        _res.Body = this;
        InitStateMachine();
    }

    public override void _PhysicsProcess(float delta)
    {
        _stateMachine.TickPhysics(delta);
    }

    private void InitStateMachine()
    {
        HeroState groundState = new GroundState();
        groundState.InitState(_res);
        _stateMachine.InsertState("idling", groundState);
        _stateMachine.MakeStateInitial("idling");
    }

    private void OnInputXChanged(float x)
    {
        _res.MotionX = x;
    }
}
