using System;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class OnewayDoor : Door
{

    private readonly Orientation _rightOrientation;
    
    public OnewayDoor(int xInGrid, int yInGrid, Orientation rightOrientation) : base(xInGrid, yInGrid)
    {
        _rightOrientation = rightOrientation;
    }

    public override void Interact()
    {
        if (State != 0)
            return;
        if (GameScreen.Character.Orientation == _rightOrientation)
            IncreaseState();
        else
            Console.WriteLine("It can't opened on this side.");
    }

}