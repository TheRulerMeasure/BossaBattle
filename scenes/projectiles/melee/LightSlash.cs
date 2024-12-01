using Godot;
using System;

public class LightSlash : Node2D
{
    [Export]
    public int SlashType { get; set; } = 0;
    [Export]
    public bool FacingRight { get; set; } = true;

    [Export]
    public DamageInfo DamageInfo { get; set; }

    public override void _Ready()
    {
        var hurtbox = GetNode<AreaHurtbox>("Area2D");
        hurtbox.DamageInfo = DamageInfo;

        GetNode<Sprite>("Sprite").FlipH = !FacingRight;
        GetNode<AnimationPlayer>("AnimationPlayer").Play(SlashType == 1 ? "slash2" : "slash");

        var tween = CreateTween().SetProcessMode(Tween.TweenProcessMode.Physics);
        tween.TweenInterval(0.16f);
        tween.TweenCallback(this, "queue_free");
    }
}
