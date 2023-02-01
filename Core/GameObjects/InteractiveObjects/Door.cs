using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Door : InteractiveObject
{
    
    internal new const string Name = "door";
    internal new const int StateNumber = 2; 
    
    public Door(Texture2D[] textures, int xInGrid, int yInGrid) : base(textures, xInGrid, yInGrid)
    {
        
    }

    public override void Interact(Character character)
    {
        if (State == 0)
            State = 1;
        UpdateTexture();
    }
    
}