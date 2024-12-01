using BossaBattle.resources.states.hero;
using Godot;
using System;

public class Hero : MobBody
{
    [Signal]
    public delegate void InflictedOtherMob(DamageInfo damageInfo);

    private Vector2 _relVel = Vector2.Zero;

    [Export]
    public Vector2 RelVel
    {
        get { return _relVel; }
        set { SetRelVel(value); }
    }

    private void SetRelVel(Vector2 vel)
    {
        _relVel = vel;
        _res.RelVel = vel;
    }

    private readonly ResHero _res = new ResHero();

    private StateMachine _stateMachine = new StateMachine();

    public override void _Ready()
    {
        _res.PackedLightSlash = GD.Load<PackedScene>("res://scenes/projectiles/melee/LightSlash.tscn");
        _res.Body = this;
        _res.Sprite = GetNode<Sprite>("Sprite");
        _res.SpriteAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _res.PhysicalAnimPlayer = GetNode<AnimationPlayer>("PhysicalAnimPlayer");
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
            new Tuple<string, HeroState>("attack_a", new AttackAState()),
            new Tuple<string, HeroState>("attack_b", new AttackBState()),
            new Tuple<string, HeroState>("air_attack_a", new AirAttackAState()),
            new Tuple<string, HeroState>("air_attack_b", new AirAttackBState()),
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

    private void OnInputAttack1()
    {
        _res.InputStrengthAttack1 = 0.12f;
    }

    private void OnInflictedOtherMob(DamageInfo damageInfo)
    {
        EmitSignal(nameof(InflictedOtherMob), damageInfo);
    }
}
