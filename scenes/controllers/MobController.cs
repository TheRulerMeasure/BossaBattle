using Godot;
using System;

public class MobController : Node
{
    [Signal]
    public delegate void InputXChanged(float x);
    [Signal]
    public delegate void InputJumpChanged(bool jump);

    [Export]
    public NodePath MobNodePath { get; set; } = "..";

    public override void _Ready()
    {
        Node mob = GetNode(MobNodePath);
        Connect(nameof(InputXChanged), mob, "OnInputXChanged");
        Connect(nameof(InputJumpChanged), mob, "OnInputJumpChanged");
    }

    public override void _Process(float delta)
    {
        EmitSignal(nameof(InputXChanged), Input.GetAxis("move_left", "move_right"));

        if (Input.IsActionJustPressed("move_jump"))
        {
            EmitSignal(nameof(InputJumpChanged), true);
        }
        else if (Input.IsActionJustReleased("move_jump"))
        {
            EmitSignal(nameof(InputJumpChanged), false);
        }
    }
}
