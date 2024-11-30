using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class GroundState : HeroState
    {
        public override string TickPhysics(float delta)
        {
            if (Res.WantsJump())
            {
                float y = -Res.JumpForce;
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, y);
                Res.Body.BodyMoveAndSlide();
                return "jumping";
            }
            if (Mathf.IsZeroApprox(Res.MotionX))
            {
                Res.Body.ApplyFriction(delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlideWithSnap();
            }
            else
            {
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlideWithSnap();
                Res.FacingRight = Res.MotionX > 0.2f;
            }
            if (!Res.Body.IsOnFloor())
            {
                return "falling";
            }
            if (Res.WantsAttack1())
            {
                return "attack_a";
            }
            return string.Empty;
        }
    }
}
