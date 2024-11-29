using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class GroundState : HeroState
    {
        public override string TickPhysics(float delta)
        {
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
