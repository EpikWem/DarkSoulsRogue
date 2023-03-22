using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Ladder : InteractiveObject
{

    public const string Name = "ladder";
    public const int StateNumber = 2;

    private readonly Destination _destination;
    
    public Ladder(int xInGrid, int yInGrid, bool isTop, Destination destination) : base(xInGrid, yInGrid, Textures.LadderT)
    {
        if (isTop)
            IncreaseState();
        _destination = destination;
    }

    public override void Interact(Character character)
    {
        Main.LoadMap(_destination.MapId);
        character.PlaceOnGrid(_destination);
    }
    
}