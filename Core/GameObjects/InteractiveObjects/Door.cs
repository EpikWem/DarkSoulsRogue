using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Door : InteractiveObject
{
    
    internal new const string Name = "door";
    internal new const int StateNumber = 2;

    public readonly Destination Destination;
    
    public Door(int xInGrid, int yInGrid, Destination destination) : base(xInGrid, yInGrid)
    {
        Destination = destination;
    }

    public override void Interact(Character character)
    {
        switch (State)
        {
            case 0: State = 1; break;
            case 1: return;
        }
        UpdateTexture();
    }
    
}