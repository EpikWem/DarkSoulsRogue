using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Wall : GameObject
{

    public Wall(Texture2D texture, int xInGrid, int yInGrid) : base(texture)
    {
        Position.X = xInGrid*GameMain.CellSize;
        Position.Y = yInGrid*GameMain.CellSize;
    }
    
}