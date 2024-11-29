using BossaBattle.resources.states.hero;
using Godot;
using System;

public class Hero : MobBody
{
    private readonly ResHero _res = new ResHero();

    private StateMachine _stateMachine = new StateMachine();

    public override void _Ready()
    {
        _res.Body = this;
        InitStateMachine();
    }

    public override void _Process(float delta)
    {
        _res.DecreaseInputStrengthAll(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        _stateMachine.TickPhysics(delta);
    }

    private void InitStateMachine()
    {
        Tuple<string, HeroState>[] states = new Tuple<string, HeroState>[]
        {
            new Tuple<string, HeroState>("ground", new GroundState()),
            new Tuple<string, HeroState>("jumping", new JumpingState()),
            new Tuple<string, HeroState>("falling", new FallingState()),
        };
        foreach (var state in states)
        {
            state.Item2.InitState(_res);
            _stateMachine.InsertState(state.Item1, state.Item2);
        }
        _stateMachine.MakeStateInitial("ground");
    }

    private void OnInputXChanged(float x)
    {
        _res.MotionX = x;
    }

    private void OnInputJumpChanged(bool jump)
    {
        _res.InputHeldJump = jump;
        if (jump)
        {
            _res.InputStrengthJump = 0.12f;
        }
        else
        {
            _res.InputStrengthJump = 0f;
        }
    }
}
