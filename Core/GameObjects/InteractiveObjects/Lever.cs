using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Lever : InteractiveObject
{
    
    public new static string Name = "lever";
    public new static int StateNumber = 2; 

    public Lever(Texture2D[] textures, int xInGrid, int yInGrid) : base(textures, xInGrid, yInGrid)
    {
        
    }

    public override void Interact(Character character)
    {
        
    }
    
}