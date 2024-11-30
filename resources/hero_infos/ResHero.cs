using Godot;
using System;

public class ResHero : Resource
{
    public MobBody Body { get; set; }

    public AnimationPlayer PhysicalAnimPlayer { get; set; }
    public AnimationPlayer SpriteAnimPlayer { get; set; }

    public Sprite Sprite { get; set; }

    public PackedScene PackedLightSlash { get; set; }

    public float JumpForce { get; set; } = 475f;
    public float FallingVelocity { get; set; } = 211f;

    public Vector2 RelVel { get; set; } = Vector2.Zero;

    public bool FacingRight { get; set; } = true;

    public float MotionX { get; set; } = 0f;
    public float InputStrengthJump { get; set; } = 0f;
    public float InputStrengthAttack1 { get; set; } = 0f;

    public bool InputHeldJump { get; set; } = false;

    public bool WantsJump()
    {
        return !Mathf.IsZeroApprox(InputStrengthJump);
    }

    public bool WantsAttack1()
    {
        return !Mathf.IsZeroApprox(InputStrengthAttack1);
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

    public void SpriteFlip(bool flip)
    {
        Sprite.FlipH = flip;
    }

    public void SlashARight(Vector2 pos)
    {
        LightSlash slash = PackedLightSlash.Instance<LightSlash>();
        slash.FacingRight = true;
        slash.SlashType = 0;
        slash.Position = pos + new Vector2(16, -22);
        Body.GetParent().AddChild(slash);
    }

    public void SlashALeft(Vector2 pos)
    {
        LightSlash slash = PackedLightSlash.Instance<LightSlash>();
        slash.FacingRight = false;
        slash.SlashType = 0;
        slash.Position = pos + new Vector2(-16, -22);
        Body.GetParent().AddChild(slash);
    }

    public void SlashBRight(Vector2 pos)
    {
        LightSlash slash = PackedLightSlash.Instance<LightSlash>();
        slash.FacingRight = true;
        slash.SlashType = 1;
        slash.Position = pos + new Vector2(16, -22);
        Body.GetParent().AddChild(slash);
    }

    public void SlashBLeft(Vector2 pos)
    {
        LightSlash slash = PackedLightSlash.Instance<LightSlash>();
        slash.FacingRight = false;
        slash.SlashType = 1;
        slash.Position = pos + new Vector2(-16, -22);
        Body.GetParent().AddChild(slash);
    }
}
