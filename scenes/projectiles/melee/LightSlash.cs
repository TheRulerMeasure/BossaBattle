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
        var sp1 = GetNode<Sprite>("Sprite");
        sp1.FlipH = !FacingRight;
        var sp2 = GetNode<Sprite>("Sprite2");
        sp2.FlipH = !FacingRight;
        switch (SlashType)
        {
            case 1:
                tween.TweenInterval(0.16f);
                sp1.Hide();
                GetNode<AnimationPlayer>("AnimationPlayer").Play("slash2");
                break;
            default:
                tween.TweenInterval(0.18f);
                sp2.Hide();
                GetNode<AnimationPlayer>("AnimationPlayer").Play("slash");
                break;
        }
        tween.TweenCallback(this, "queue_free");
    }
}
