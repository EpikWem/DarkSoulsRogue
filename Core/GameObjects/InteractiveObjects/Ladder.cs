﻿using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Ladder : InteractiveObject
{

    public const string Name = "ladder";
    public const int StateNumber = 1;

    private readonly Destination _destination;
    
    public Ladder(int xInGrid, int yInGrid, bool isTop, Destination destination)
        : base(xInGrid, yInGrid, isTop ? Textures.LadderTopT : Textures.LadderBottomT)
    {
        _destination = destination;
    }

    public override void Interact()
    {
        GameScreen.LoadMap(_destination.MapId);
        GameScreen.Character.PlaceOnGrid(_destination);
    }
    
}