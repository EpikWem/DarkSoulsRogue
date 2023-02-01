using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Ladder : InteractiveObject
{
    
    internal new const string Name = "ladder";
    internal new const int StateNumber = 1; 

    private readonly int _destinationMap;
    
    public Ladder(Texture2D[] textures, int xInGrid, int yInGrid, int destinationMap) : base(textures, xInGrid, yInGrid)
    {
        _destinationMap = destinationMap;
    }

    public override void Interact(Character character)
    {
        
    }
    
}