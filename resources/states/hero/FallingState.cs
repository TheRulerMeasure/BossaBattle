using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class FallingState : HeroState
    {
        private float coyoteTime = 0f;

        public override void Enter(string prevStateKey)
        {
            if (prevStateKey == "ground")
            {
                coyoteTime = 0.175f;
            }
        }

        public override string TickPhysics(float delta)
        {
            if (!Mathf.IsZeroApprox(coyoteTime) && Res.WantsJump())
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
                Res.Body.BodyMoveAndSlide();
            }
            else
            {
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.ApplyGravity(delta);
                Res.Body.BodyMoveAndSlide();
            }
            if (Res.Body.IsOnFloor())
            {
                return "ground";
            }
            coyoteTime = Mathf.MoveToward(coyoteTime, 0f, delta);
            return string.Empty;
        }

        public override void Exit()
        {
            coyoteTime = 0f;
        }
    }
}
