using Godot;
using System;

public class ResHero : Resource
{
    public MobBody Body { get; set; }

    public float JumpForce { get; set; } = 678f;
    public float FallingVelocity { get; set; } = 350f;

    public float MotionX { get; set; } = 0f;
    public float InputStrengthJump { get; set; } = 0f;

    public bool InputHeldJump { get; set; } = false;

    public bool WantsJump()
    {
        return !Mathf.IsZeroApprox(InputStrengthJump);
    }

    public void DecreaseInputStrengthAll(float delta)
    {
        InputStrengthJump = Mathf.MoveToward(InputStrengthJump, 0f, delta);
    }
}
