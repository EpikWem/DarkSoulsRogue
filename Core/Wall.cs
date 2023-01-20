using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Wall : GameObject
{

    public Wall(Texture2D texture, int x, int y) : base(texture)
    {
        Position.X = x;
        Position.Y = y;
    }
    
}