using Godot;
using Godot.Collections;

public class Gold : KinematicBody2D
{
    [Export]
    public Vector2 Velocity { get; set; } = Vector2.Zero;

    private bool _collected = false;

    public override void _Ready()
    {
        var tween = CreateTween();
        tween.TweenInterval(0.26f);
        tween.TweenCallback(GetNode("Area2D/CollisionShape2D"), "set_deferred", new Array("disabled", false));
        var area = GetNode<Area2D>("Area2D");
        area.Connect("Collected", this, nameof(OnCollected));
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

    private void OnCollected()
    {
        if (_collected)
        {
            return;
        }
        _collected = true;
        SetPhysicsProcess(false);
        var tween = CreateTween();
        tween.TweenProperty(GetNode("Sprite"), "position", Vector2.Up * 46f, 0.5f)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Expo);
        tween.TweenCallback(this, "queue_free");
    }
}
