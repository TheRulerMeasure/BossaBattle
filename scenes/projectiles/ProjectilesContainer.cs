using Godot;
using System;

public class ProjectilesContainer : Node2D
{
    public override void _Ready()
    {
        
    }

    private void OnRequestedPutProjectileAt(Node2D scene, Vector2 pos)
    {
        scene.Position = pos;
        AddChild(scene);
    }
}
