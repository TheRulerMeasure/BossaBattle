using Godot;
using System;

public class DamageInfo : Resource
{
    [Export]
    public int Damage { get; set; } = 1;

    [Export]
    public Vector2 ForceImpulse { get; set; } = Vector2.Zero;

    public Node Inflictor { get; set; }
}
