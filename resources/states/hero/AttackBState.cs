using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class AttackBState : HeroState
    {
        private const float MAX_ATTACK_TIME = 0.33f;

        private float _attackTime = 0f;

        public override void Enter(string prevStateKey)
        {
            Res.PhysicalAnimPlayer.Play("attack2");
            Res.SpriteAnimPlayer.Play("attack");
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
            Res.Body.BodyMoveAndSlideWithSnap(Res.GetFacingRelVel());
            _attackTime += delta;
            if (_attackTime >= MAX_ATTACK_TIME)
            {
                if (Res.Body.IsOnFloor())
                {
                    return "ground";
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
