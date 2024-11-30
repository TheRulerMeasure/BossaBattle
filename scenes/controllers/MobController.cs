using Godot;
using System;

public class MobController : Node
{
    [Signal]
    public delegate void InputXChanged(float x);
    [Signal]
    public delegate void InputJumpChanged(bool jump);
    [Signal]
    public delegate void InputAttack1();

    [Export]
    public NodePath MobNodePath { get; set; } = "..";

    public override void _Ready()
    {
        Node mob = GetNode(MobNodePath);
        Connect(nameof(InputXChanged), mob, "OnInputXChanged");
        Connect(nameof(InputJumpChanged), mob, "OnInputJumpChanged");
        Connect(nameof(InputAttack1), mob, "OnInputAttack1");
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("move_right"))
        {
            EmitSignal(nameof(InputXChanged), 1f);
        }
        else if (Input.IsActionPressed("move_left"))
        {
            EmitSignal(nameof(InputXChanged), -1f);
        }
        else
        {
            EmitSignal(nameof(InputXChanged), 0f);
        }

        if (Input.IsActionJustPressed("move_jump"))
        {
            EmitSignal(nameof(InputJumpChanged), true);
        }
        else if (Input.IsActionJustReleased("move_jump"))
        {
            EmitSignal(nameof(InputJumpChanged), false);
        }

        if (Input.IsActionJustPressed("attack1"))
        {
            EmitSignal(nameof(InputAttack1));
        }
    }
}
