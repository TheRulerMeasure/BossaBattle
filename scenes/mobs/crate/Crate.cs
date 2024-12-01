using Godot;
using System;

public class Crate : MobBody
{
    [Export(PropertyHint.Range, "1,100")]
    public int Health { get; set; } = 5;

    [Export(PropertyHint.Range, "0,50")]
    public int Golds { get; set; } = 6;

    private float _hurtTime = 0f;

    private ShaderMaterial _shader;

    private PackedScene _packedGold = GD.Load<PackedScene>("res://scenes/mobs/gold/Gold.tscn");
    private PackedScene _packedCrateExplosion = GD.Load<PackedScene>("res://scenes/mobs/crate/CrateExplosion.tscn");

    private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        _rng.Randomize();
        _rng.Seed += 15611;
        _shader = GetNode<Sprite>("Sprite").Material as ShaderMaterial;
    }

    public override void _Process(float delta)
    {
        if (_hurtTime >= 0f)
        {
            float amount = Mathf.Sin(_hurtTime * 100f) + 1f;
            amount *= 0.5f;
            _shader.SetShaderParam("amount", amount);
            _hurtTime -= delta;
        }
        else
        {
            _shader.SetShaderParam("amount", 0f);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        ApplyFriction(delta);
        ApplyGravity(delta);
        BodyMoveAndSlide();
    }

    private void SpitGolds()
    {
        var explosion = _packedCrateExplosion.Instance<Node2D>();
        explosion.Position = Position;
        GetParent().CallDeferred("add_child", explosion);
        for (int i = 0; i < Golds; i++)
        {
            Gold gold = _packedGold.Instance<Gold>();
            gold.Position = Position;
            float rotation = Mathf.Pi * (0.4f + _rng.Randf() * 0.2f);
            float force = 200f + _rng.Randf() * 200f;
            gold.Velocity = Vector2.Right.Rotated(rotation) * force;
            GetParent().CallDeferred("add_child", gold);
        }
    }

    private void OnTakenDamage(DamageInfo damageInfo)
    {
        Health -= damageInfo.Damage;
        if (Health <= 0)
        {
            SpitGolds();
            QueueFree();
            return;
        }
        Velocity = damageInfo.ForceImpulse;
        _hurtTime = 0.5f;
    }
}
