﻿using DarkSoulsRogue.Core.Statics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Door : InteractiveObject
{
    
    public const string Name = "door";
    public const int StateNumber = 2;
    
    public Door(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.DoorT)
    {
        
    }

    public override void Interact()
    {
        if (State != 0)
            return;
        Sounds.Play(Sounds.SChest);
        IncreaseState();
    }
    
}