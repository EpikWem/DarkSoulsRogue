using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Wall : GameObject
{

    public Wall(int xInGrid, int yInGrid, Texture2D texture) : base(texture)
    {
        Position.X = xInGrid*Camera.CellSize;
        Position.Y = yInGrid*Camera.CellSize;
    }
    
}