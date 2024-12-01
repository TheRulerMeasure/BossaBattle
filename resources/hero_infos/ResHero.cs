using Godot;
using System;

public class ResHero : Resource
{
    [Signal]
    public delegate void HealthChanged(int health);

    public MobBody Body { get; set; }

    public AnimationPlayer PhysicalAnimPlayer { get; set; }
    public AnimationPlayer SpriteAnimPlayer { get; set; }

    public Sprite Sprite { get; set; }

    public PackedScene PackedLightSlash { get; set; }

    public int Health { get; set; } = 5;

    public float JumpForce { get; set; } = 475f;
    public float FallingVelocity { get; set; } = 211f;

    public Vector2 RelVel { get; set; } = Vector2.Zero;

    public bool FacingRight { get; set; } = true;

    public float MotionX { get; set; } = 0f;
    public float InputStrengthJump { get; set; } = 0f;
    public float InputStrengthAttack1 { get; set; } = 0f;

    public float InvincibilityTime { get; set; } = 0f;

    public bool InputHeldJump { get; set; } = false;

    public bool WantsJump()
    {
        return !Mathf.IsZeroApprox(InputStrengthJump);
    }

    public bool WantsAttack1()
    {
        return !Mathf.IsZeroApprox(InputStrengthAttack1);
    }

    public bool IsInvincible()
    {
        return !Mathf.IsZeroApprox(InvincibilityTime);
    }

    public Vector2 GetFacingRelVel()
    {
        return RelVel * (FacingRight ? new Vector2(1, 1) : new Vector2(-1, 1));
    }

    public void DecreaseInputStrengthAll(float delta)
    {
        InputStrengthJump = Mathf.MoveToward(InputStrengthJump, 0f, delta);
        InputStrengthAttack1 = Mathf.MoveToward(InputStrengthAttack1, 0f, delta);
    }

    public void DecreaseInvincibility(float delta)
    {
        InvincibilityTime = Mathf.MoveToward(InvincibilityTime, 0f, delta);
    }

    public void SpriteFlip(bool flip)
    {
        Sprite.FlipH = flip;
    }

    public void SlashAttack(Vector2 pos, DamageInfo damageInfo, bool rightSide, int type)
    {
        LightSlash slash = PackedLightSlash.Instance<LightSlash>();
        slash.FacingRight = rightSide;
        slash.SlashType = type;
        slash.Position = pos + new Vector2(rightSide ? 16 : -16, -20);
        slash.DamageInfo = damageInfo;
        Body.GetParent().AddChild(slash);
    }

    public void SlashARight(Vector2 pos)
    {
        var damageInfo = new DamageInfo
        {
            Inflictor = Body,
            Damage = 1,
            ForceImpulse = Vector2.Right.Rotated(Mathf.Pi * -0.25f) * 221f,
        };
        SlashAttack(pos, damageInfo, true, 0);
    }

    public void SlashALeft(Vector2 pos)
    {
        var damageInfo = new DamageInfo
        {
            Inflictor = Body,
            Damage = 1,
            ForceImpulse = Vector2.Right.Rotated(Mathf.Pi * -0.75f) * 221f,
        };
        SlashAttack(pos, damageInfo, false, 0);
    }

    public void SlashBRight(Vector2 pos)
    {
        var damageInfo = new DamageInfo
        {
            Inflictor = Body,
            Damage = 1,
            ForceImpulse = Vector2.Right.Rotated(Mathf.Pi * -0.25f) * 221f,
        };
        SlashAttack(pos, damageInfo, true, 1);
    }

    public void SlashBLeft(Vector2 pos)
    {
        var damageInfo = new DamageInfo
        {
            Inflictor = Body,
            Damage = 1,
            ForceImpulse = Vector2.Right.Rotated(Mathf.Pi * -0.75f) * 221f,
        };
        SlashAttack(pos, damageInfo, false, 1);
    }
}
