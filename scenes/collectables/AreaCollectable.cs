using Godot;
using System;

public class AreaCollectable : Area2D
{
    [Signal]
    public delegate void Collected();

    public override void _Ready()
    {
        
    }

    public void Collect()
    {
        EmitSignal(nameof(Collected));
    }
}
