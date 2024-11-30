using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class FallingState : HeroState
    {
        private bool _canAttack = false;

        private float _coyoteTime = 0f;

        public override void Enter(string prevStateKey)
        {
            _canAttack = true;
            if (prevStateKey == "ground")
            {
                _coyoteTime = 0.175f;
            }
            else if (prevStateKey == "air_attack_a" || prevStateKey == "air_attack_b")
            {
                _canAttack = false;
            }
            Res.SpriteAnimPlayer.Play("jump");
        }

        public override string TickPhysics(float delta)
        {
            if (!Mathf.IsZeroApprox(_coyoteTime) && Res.WantsJump())
            {
                float y = -Res.JumpForce;
                Res.Body.Velocity = new Vector2(Res.Body.Velocity.x, y);
                Res.Body.BodyMoveAndSlide();
                return "jumping";
            }
            bool hasMotion = Motioning(delta);
            if (hasMotion)
            {
                SetFacingFromMotionX(Res.MotionX);
            }
            if (Res.Body.IsOnFloor())
            {
                return "ground";
            }
            if (Res.WantsAttack1() && _canAttack)
            {
                return "air_attack_a";
            }
            _coyoteTime = Mathf.MoveToward(_coyoteTime, 0f, delta);
            return string.Empty;
        }

        public override void Exit()
        {
            _coyoteTime = 0f;
        }
    }
}
