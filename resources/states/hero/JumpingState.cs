using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class JumpingState : HeroState
    {
        public override void Enter(string prevStateKey)
        {
            Res.SpriteAnimPlayer.Play("jump");
        }

        public override string TickPhysics(float delta)
        {
            bool isFalling;
            isFalling = HandleFallVelY(delta);
            if (isFalling)
            {
                return "falling";
            }
            isFalling = HandleFallLetGoJump(delta);
            if (isFalling)
            {
                return "falling";
            }
            bool hasMotion = Motioning(delta);
            if (hasMotion)
            {
                SetFacingFromMotionX(Res.MotionX);
            }
            if (Res.WantsAttack1())
            {
                return "air_attack_a";
            }
            return string.Empty;
        }

        private bool HandleFallVelY(float delta)
        {
            if (Res.Body.Velocity.y >= -Res.FallingVelocity)
            {
                bool hasMotion1 = Motioning(delta);
                if (hasMotion1)
                {
                    SetFacingFromMotionX(Res.MotionX);
                }
                return true;
            }
            return false;
        }

        private bool HandleFallLetGoJump(float delta)
        {
            if (Res.InputHeldJump)
            {
                return false;
            }
            if (Mathf.IsZeroApprox(Res.MotionX))
            {
                Res.Body.ApplyFriction(delta);
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.FallingVelocity);
                Res.Body.BodyMoveAndSlide();
            }
            else
            {
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.FallingVelocity);
                Res.Body.BodyMoveAndSlide();
                SetFacingFromMotionX(Res.MotionX);
            }
            return true;
        }
    }
}
