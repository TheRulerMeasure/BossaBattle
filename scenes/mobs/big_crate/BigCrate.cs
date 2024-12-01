using Godot;
using System;

public class BigCrate : Node2D
{
    [Signal]
    public delegate void Died();

    private const int GAME_WIDTH = 256;

    [Export(PropertyHint.Range, "1,100")]
    public int Health = 79;

    private float _hurtTime = 0f;
    private float _attackPercent = 0f;
    private float _attackTime = 0f;

    private int _detectedCount = 0;

    private Sprite _sprite;
    private ShaderMaterial _shader;
    private CPUParticles2D _particles;

    private BigCrateStates _currentState = BigCrateStates.Idling;

    private readonly PackedScene _packedFallingCrate = GD.Load<PackedScene>("res://scenes/mobs/falling_crate/FallingCrate.tscn");

    private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        _rng.Randomize();
        _rng.Seed += 1113;
        var enemyDetector = GetNode<Area2D>("Area2D2");
        enemyDetector.Connect("area_entered", this, nameof(OnEnemyDetectorAreaEntered));
        enemyDetector.Connect("area_exited", this, nameof(OnEnemyDetectorAreaExited));
        _sprite = GetNode<Sprite>("Sprite");
        _shader = GetNode<Sprite>("Sprite2").Material as ShaderMaterial;
        _particles = GetNode<CPUParticles2D>("CPUParticles2D");
    }

    public override void _Process(float delta)
    {
        _hurtTime = Mathf.MoveToward(_hurtTime, 0f, delta);
        if (Mathf.IsZeroApprox(_hurtTime))
        {
            _shader.SetShaderParam("amount", 0f);
        }
        else
        {
            float amount = Mathf.Sin(_hurtTime * 100f) + 1f;
            amount *= 0.5f;
            _shader.SetShaderParam("amount", amount);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        BigCrateStates newState;
        switch (_currentState)
        {
            case BigCrateStates.PreparingAttack:
                newState = PhysProcessPreparingAttack(delta);
                break;
            case BigCrateStates.Attacking:
                newState = PhysProcessAttacking(delta);
                break;
            default:
                newState = PhysProcessIdling(delta);
                break;
        }
        if (newState != BigCrateStates.None)
        {
            _currentState = newState;
        }
    }

    private BigCrateStates PhysProcessIdling(float delta)
    {
        if (_detectedCount > 0)
        {
            return BigCrateStates.PreparingAttack;
        }
        return BigCrateStates.None;
    }

    private BigCrateStates PhysProcessPreparingAttack(float delta)
    {
        if (_detectedCount <= 0)
        {
            return BigCrateStates.Idling;
        }
        _attackPercent += 0.2f * delta;
        if (_attackPercent >= 1f)
        {
            _attackPercent = 0f;
            _attackTime = 3f;
            _sprite.Frame = 1;
            _particles.Emitting = true;
            return BigCrateStates.Attacking;
        }
        return BigCrateStates.None;
    }

    private BigCrateStates PhysProcessAttacking(float delta)
    {
        _attackTime -= delta;
        if (_attackTime <= 0f)
        {
            _sprite.Frame = 0;
            RainCrates();
            _particles.Emitting = false;
            return BigCrateStates.PreparingAttack;
        }
        return BigCrateStates.None;
    }

    private void PutFallingCrateAt(Vector2 pos)
    {
        var fallingCrate = _packedFallingCrate.Instance<FallingCrate>();
        fallingCrate.Position = pos;
        GetParent().AddChild(fallingCrate);
    }

    private void RainCrates()
    {
        float y = -197f;
        for (int i = 0; i < 14; i++)
        {
            float x = (_rng.Randf() * GAME_WIDTH) - GAME_WIDTH * 0.5f;
            y -= 5f + _rng.Randf() * 20f;
            PutFallingCrateAt(Position + new Vector2(x, y));
        }
    }

    private void OnTakenDamage(DamageInfo damageInfo)
    {
        Health -= damageInfo.Damage;
        if (IsInstanceValid(damageInfo.Inflictor))
        {
            if (damageInfo.Inflictor.HasMethod("InflictedOtherBody"))
            {
                damageInfo.Inflictor.Call("InflictedOtherBody", this, damageInfo);
            }
        }
        if (Health <= 0)
        {
            QueueFree();
            EmitSignal(nameof(Died));
            return;
        }
        _hurtTime = 0.1f;
    }

    private void OnEnemyDetectorAreaEntered(Area2D area)
    {
        _detectedCount++;
    }

    private void OnEnemyDetectorAreaExited(Area2D area)
    {
        _detectedCount--;
    }
}
