using Godot;
using System;

public class CellCam : Camera2D
{
    private const int GAME_WIDTH = 256;
    private const int GAME_HEIGHT = 256;

    [Export]
    public NodePath TrackingMob { get; set; }

    private Node2D _target;

    private SceneTreeTween _swipeTween;

    private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        _target = GetNode<Node2D>(TrackingMob);
        _target.Connect("InflictedOtherMob", this, nameof(OnMobInflictedOtherMob));
        _swipeTween = CreateTween();
        _swipeTween.TweenInterval(0.1f);
    }

    public override void _Process(float delta)
    {
        Vector2 cellV = (_target.Position / new Vector2(GAME_WIDTH, GAME_HEIGHT)).Floor();
        Position = cellV * new Vector2(GAME_WIDTH, GAME_HEIGHT) + new Vector2(GAME_WIDTH * 0.5f, GAME_HEIGHT * 0.5f);
    }

    public void SwipeDirection(Vector2 impulseForce)
    {
        _swipeTween.Kill();
        _swipeTween = CreateTween();
        _swipeTween.TweenProperty(this, "offset", impulseForce, 0.15f)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Expo);
        _swipeTween.TweenProperty(this, "offset", Vector2.Zero, 0.3f)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Quad);
    }

    private void OnMobInflictedOtherMob(DamageInfo damageInfo)
    {
        Vector2 forceImpulse = damageInfo.ForceImpulse;
        if (Mathf.IsZeroApprox(forceImpulse.x))
        {
            return;
        }
        Vector2 dir = Vector2.Right * Mathf.Sign(forceImpulse.x);
        dir = dir.Rotated(_rng.Randf() * Mathf.Pi * (_rng.Randf() < 0.5f ? -0.2f : 0.2f));
        SwipeDirection(dir * (100f + _rng.Randf() * 200f));
    }
}
