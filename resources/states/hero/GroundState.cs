using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class GroundState : HeroState
    {
        private bool _moving = false;

        public override void Enter(string prevStateKey)
        {
            _moving = false;
            Res.SpriteAnimPlayer.Play("idle");
        }

        public override string TickPhysics(float delta)
        {
            if (Res.WantsJump())
            {
                if (Mathf.IsZeroApprox(Res.MotionX))
                {
                    Res.Body.ApplyFriction(delta);
                    Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.JumpForce);
                    Res.Body.BodyMoveAndSlide();
                    return "jumping";
                }
                Res.Body.ApplyMotion(Res.MotionX, delta);
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, -Res.JumpForce);
                Res.Body.BodyMoveAndSlide();
                return "jumping";
            }
            bool oldMoving = _moving;
            bool hasMotion = MotioningWithSnap(delta);
            if (hasMotion)
            {
                SetFacingFromMotionX(Res.MotionX);
            }
            _moving = hasMotion;
            if (_moving && !oldMoving)
            {
                Res.SpriteAnimPlayer.Play("walk");
            }
            else if (!_moving && oldMoving)
            {
                Res.SpriteAnimPlayer.Play("idle");
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
