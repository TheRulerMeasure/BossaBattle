using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class AirAttackAState : HeroState
    {
        private const float MAX_ATTACK_TIME = 0.2f;

        private float _attackTime = 0f;

        public override void Enter(string prevStateKey)
        {
            Res.PhysicalAnimPlayer.Play("air_attack1");
            if (Res.FacingRight)
            {
                Res.SlashARight(Res.Body.Position);
            }
            else
            {
                Res.SlashALeft(Res.Body.Position);
            }
        }

        public override string TickPhysics(float delta)
        {
            Res.Body.BodyMoveAndSlide(Res.GetFacingRelVel());
            _attackTime += delta;
            if (_attackTime >= MAX_ATTACK_TIME)
            {
                if (Res.WantsAttack1())
                {
                    return "air_attack_b";
                }
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
