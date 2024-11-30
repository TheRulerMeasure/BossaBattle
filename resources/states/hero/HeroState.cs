using Godot;
using System;

public class HeroState : BaseState
{
    protected ResHero Res;

    public void InitState(ResHero res)
    {
        Res = res;
    }

    public bool Motioning(float delta)
    {
        if (Mathf.IsZeroApprox(Res.MotionX))
        {
            Res.Body.ApplyFriction(delta);
            Res.Body.ApplyGravity(delta);
            Res.Body.BodyMoveAndSlide();
            return false;
        }
        Res.Body.ApplyMotion(Res.MotionX, delta);
        Res.Body.ApplyGravity(delta);
        Res.Body.BodyMoveAndSlide();
        return true;
    }

    public bool MotioningWithSnap(float delta)
    {
        if (Mathf.IsZeroApprox(Res.MotionX))
        {
            Res.Body.ApplyFriction(delta);
            Res.Body.ApplyGravity(delta);
            Res.Body.BodyMoveAndSlideWithSnap();
            return false;
        }
        Res.Body.ApplyMotion(Res.MotionX, delta);
        Res.Body.ApplyGravity(delta);
        Res.Body.BodyMoveAndSlideWithSnap();
        return true;
    }

    public void SetFacingFromMotionX(float x)
    {
        Res.FacingRight = x > -0.2f;
        Res.SpriteFlip(x < -0.2f);
    }
}
