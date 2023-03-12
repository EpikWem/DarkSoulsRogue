using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Door : InteractiveObject
{
    
    public const string Name = "door";
    public const int StateNumber = 2;

    public readonly Destination Destination;
    
    public Door(int xInGrid, int yInGrid, Destination destination) : base(xInGrid, yInGrid, Textures.DoorT)
    {
        Destination = destination;
    }

    public override void Interact(Character character)
    {
        IncreaseState();
    }
    
}