using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Wall : GameObject
{
    
    protected Wall()
    {
        
    }

    public Wall(Texture2D texture, int xInGrid, int yInGrid) : base(texture)
    {
        Position.X = xInGrid*Main.CellSize;
        Position.Y = yInGrid*Main.CellSize;
    }
    
}