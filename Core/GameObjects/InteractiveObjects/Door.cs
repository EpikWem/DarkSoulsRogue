using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;

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
        if (State == 0)
            IncreaseState();
    }
    
}