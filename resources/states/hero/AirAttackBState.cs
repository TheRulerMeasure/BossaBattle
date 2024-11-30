using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class AirAttackBState : HeroState
    {
        private const float MAX_ATTACK_TIME = 0.3f;

        private float _attackTime = 0f;

        public override void Enter(string prevStateKey)
        {
            Res.PhysicalAnimPlayer.Play("air_attack2");
            Res.SpriteAnimPlayer.Play("air_attack");
            if (Res.FacingRight)
            {
                Res.SlashBRight(Res.Body.Position);
            }
            else
            {
                Res.SlashBLeft(Res.Body.Position);
            }
        }

        public override string TickPhysics(float delta)
        {
            Res.Body.BodyMoveAndSlide(Res.GetFacingRelVel());
            _attackTime += delta;
            if (_attackTime >= MAX_ATTACK_TIME)
            {
                return "falling";
            }
            return string.Empty;
        }

        public override void Exit()
        {
            _attackTime = 0f;
            Res.PhysicalAnimPlayer.Play("RESET");
        }
    }
}
