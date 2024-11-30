using Godot;
using System;

namespace BossaBattle.resources.states.hero
{
    public class AttackAState : HeroState
    {
        private const float MAX_ATTACK_TIME = 0.37f;
        private const float MAX_CONTINUE_TIME = 0.25f;

        private float _attackTime = 0f;

        public override void Enter(string prevStateKey)
        {
            Res.PhysicalAnimPlayer.Play("attack1");
            Res.SpriteAnimPlayer.Play("attack");
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
            Res.Body.BodyMoveAndSlideWithSnap(Res.GetFacingRelVel());
            _attackTime += delta;
            if (_attackTime > MAX_CONTINUE_TIME)
            {
                if (Res.WantsAttack1() && Res.Body.IsOnFloor())
                {
                    return "attack_b";
                }
            }
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
