using Godot;
using System;

public class AreaHitbox : Area2D
{
    [Signal]
    public delegate void TakenDamage(DamageInfo damageInfo);

    public override void _Ready()
    {
        Connect(nameof(TakenDamage), Owner, "OnTakenDamage");
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        EmitSignal(nameof(TakenDamage), damageInfo);
    }
}
