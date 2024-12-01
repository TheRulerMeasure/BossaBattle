using Godot;
using System;

public class FallingCrate : Node2D
{
    private RayCast2D _rayCast;

    public override void _Ready()
    {
        _rayCast = GetNode<RayCast2D>("RayCast2D");
        AreaHurtbox area = GetNode<AreaHurtbox>("Area2D");
        area.DamageInfo = new DamageInfo
        {
            Inflictor = this,
            Damage = 1,
            ForceImpulse = Vector2.Zero,
        };
    }

    public override void _PhysicsProcess(float delta)
    {
        Position += Vector2.Down * 57f * delta;
        if (_rayCast.IsColliding())
        {
            QueueFree();
        }
    }
}
