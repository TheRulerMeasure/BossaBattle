using Godot;
using System;

public class HeroState : BaseState
{
    protected ResHero Res;

    public void InitState(ResHero res)
    {
        Res = res;
    }
}
