using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Wall : GameObject
{

    public Wall(int xInGrid, int yInGrid, Texture2D texture) : base(texture)
    {
        Position.X = xInGrid*Main.CellSize;
        Position.Y = yInGrid*Main.CellSize;
    }
    
}