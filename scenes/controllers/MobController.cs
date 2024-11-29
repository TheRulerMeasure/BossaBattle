using Godot;
using System;

public class MobController : Node
{
    [Signal]
    public delegate void InputXChanged(float x);

    [Export]
    public NodePath MobNodePath { get; set; } = "..";

    public override void _Ready()
    {
        Node mob = GetNode(MobNodePath);
        Connect(nameof(InputXChanged), mob, "OnInputXChanged");
    }

    public override void _Process(float delta)
    {
        EmitSignal(nameof(InputXChanged), Input.GetAxis("move_left", "move_right"));
    }
}
