using Godot;
using System;

public class Gold : KinematicBody2D
{
    [Export]
    public Vector2 Velocity { get; set; } = Vector2.Zero;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        ApplyGravity(delta);
        float oldVelX = Velocity.x;
        float oldVelY = Velocity.y;
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
        for (int i = 0; i < GetSlideCount(); i++)
        {
            var col = GetSlideCollision(i);
            if (Mathf.Abs(col.Normal.x) > 0.2f)
            {
                Velocity = new Vector2(oldVelX * -1f, Velocity.y);
            }
            if (Mathf.Abs(col.Normal.y) > 0.2f)
            {
                if (Mathf.Abs(oldVelY) >= 20f)
                {
                    Velocity = new Vector2(Velocity.x, oldVelY * -0.8f);
                }
                else
                {
                    Velocity = Vector2.Zero;
                }
            }
        }
    }

    private void ApplyGravity(float delta)
    {
        float y = Velocity.y;
        y += 898f * delta;
        Velocity = new Vector2(Velocity.x, y);
    }
}
