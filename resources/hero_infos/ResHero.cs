using Godot;
using System;

public class ResHero : Resource
{
    public MobBody Body { get; set; }

    public float MotionX { get; set; } = 0f;
    public float InputStrengthJump { get; set; } = 0f;
}
