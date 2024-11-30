using Godot;
using System;

public class MobBody : KinematicBody2D
{
    [Export(PropertyHint.Range, "0.2,9999,0.2")]
    public float Acceleration { get; set; } = 2789f;
    [Export(PropertyHint.Range, "0.2,9999,0.2")]
    public float Friction { get; set; } = 1121f;
    [Export(PropertyHint.Range, "0.2,9999,0.2")]
    public float MaxSpeed { get; set; } = 199f;
    [Export(PropertyHint.Range, "0.2,9999,0.2")]
    public float Gravity { get; set; } = 1668f;
    [Export(PropertyHint.Range, "0.2,9999,0.2")]
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
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
    }

    public void BodyMoveAndSlide(Vector2 vel)
    {
        Velocity = MoveAndSlide(vel, Vector2.Up);
    }

    public void BodyMoveAndSlideWithSnap()
    {
        Velocity = MoveAndSlideWithSnap(Velocity, Vector2.Down * 10f, Vector2.Up);
    }

    public void BodyMoveAndSlideWithSnap(Vector2 vel)
    {
        Velocity = MoveAndSlideWithSnap(vel, Vector2.Down * 10f, Vector2.Up);
    }
}
