using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class JumpingState : HeroState
    {
        public override string TickPhysics(float delta)
        {
            if (Res.Body.Velocity.y >= -Res.FallingVelocity)
            {
                if (Mathf.IsZeroApprox(Res.MotionX))
                {
                    Res.Body.ApplyFriction(delta);
                    Res.Body.ApplyGravity(delta);
                    Res.Body.BodyMoveAndSlide();
                    return "falling";
                }
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlide();
                return "falling";
            }
            if (!Res.InputHeldJump)
            {
                if (Mathf.IsZeroApprox(Res.MotionX))
                {
                    Res.Body.ApplyFriction(delta);
                    Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.FallingVelocity);
                    Res.Body.BodyMoveAndSlide();
                    return "falling";
                }
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.FallingVelocity);
                Res.Body.BodyMoveAndSlide();
                return "falling";
            }
            if (Mathf.IsZeroApprox(Res.MotionX))
            {
                Res.Body.ApplyFriction(delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlide();
            }
            else
            {
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlide();
            }
            return string.Empty;
        }
    }
}