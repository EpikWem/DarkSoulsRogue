using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Ladder : Door
{
    
    internal new const string Name = "ladder";
    internal new const int StateNumber = 1;
    
    public Ladder(int xInGrid, int yInGrid, Destination destination) : base(xInGrid, yInGrid, destination)
    {
        
    }

}