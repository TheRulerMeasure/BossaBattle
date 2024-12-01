using Godot;
using System;

public class CrateExplosion : Node2D
{
    public override void _Ready()
    {
        GetNode<CPUParticles2D>("CPUParticles2D").Emitting = true;
        var tween = CreateTween();
        tween.TweenInterval(3f);
        tween.TweenCallback(this, "queue_free");
    }
}
