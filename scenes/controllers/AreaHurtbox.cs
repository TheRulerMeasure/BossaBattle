using Godot;
using System;

public class AreaHurtbox : Area2D
{
    [Export]
    public DamageInfo DamageInfo { get; set; }

    public override void _Ready()
    {
        Connect("area_entered", this, nameof(OnAreaEntered));
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is AreaHitbox areaHitbox)
        {
            areaHitbox.TakeDamage(DamageInfo);
        }
    }
}
