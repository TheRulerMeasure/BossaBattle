using Godot;
using System;

public class MobBody : KinematicBody2D
{
    [Export]
    public float Acceleration { get; set; } = 2597f;
    [Export]
    public float Friction { get; set; } = 1121f;
    [Export]
    public float MaxSpeed { get; set; } = 199f;
    [Export]
    public float Gravity { get; set; } = 600f;
    [Export]
    public float MaxFallSpeed { get; set; } = 1200f;

    [Export]
    public Vector2 Velocity { get; set; } = Vector2.Zero;

    public void ApplyMotion(float motionX, float delta)
    {
        float x = Velocity.x;
        x += motionX * Acceleration * delta;
        x = Mathf.Clamp(x, -MaxSpeed, MaxSpeed);
        Velocity = new Vector2(x, Velocity.y);
    }

    public void ApplyFriction(float delta)
    {
        float x = Velocity.x;
        x = Mathf.MoveToward(x, 0, Friction * delta);
        Velocity = new Vector2(x, Velocity.y);
    }

    public void ApplyGravity(float delta)
    {
        float y = Velocity.y;
        y += Gravity * delta;
        y = Mathf.Min(y, MaxFallSpeed);
        Velocity = new Vector2(Velocity.x, y);
    }

    public void BodyMoveAndSlide()
    {
        Velocity = MoveAndSlideWithSnap(Velocity, Vector2.Down * 10f, Vector2.Up);
    }
}
