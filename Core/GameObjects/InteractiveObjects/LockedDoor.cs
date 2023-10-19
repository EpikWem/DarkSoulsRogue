using System;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class LockedDoor : Door
{

    private readonly Key _key;
    
    public LockedDoor(int xInGrid, int yInGrid, Key key) : base(xInGrid, yInGrid)
    {
        _key = key;
    }

    public override void Interact()
    {
        if (State != 0)
            return;
        if (GameScreen.Character.Inventory.GetQuantity(_key) > 0)
            IncreaseState();
        else
            Console.WriteLine("It is locked. You need a key");
    }
    
}