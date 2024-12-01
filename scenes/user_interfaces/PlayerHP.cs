using Godot;
using System;

public class PlayerHP : MarginContainer
{
    private TextureProgress _progressBar;

    public override void _Ready()
    {
        _progressBar = GetNode<TextureProgress>("CenterContainer/TextureProgress");
    }

    private void OnPlayerHealthChanged(int hp)
    {
        _progressBar.Value = hp;
    }
}
