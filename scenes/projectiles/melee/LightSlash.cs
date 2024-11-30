using Godot;
using System;

public class LightSlash : Node2D
{
    [Export]
    public int SlashType { get; set; } = 0;
    [Export]
    public bool FacingRight { get; set; } = true;

    public override void _Ready()
    {
        var tween = CreateTween().SetProcessMode(Tween.TweenProcessMode.Physics);
        tween.TweenInterval(0.16f);
        tween.TweenCallback(this, "queue_free");
        GetNode<Sprite>("Sprite").FlipH = !FacingRight;
        GetNode<AnimationPlayer>("AnimationPlayer").Play(SlashType == 1 ? "slash2" : "slash");
    }
}
