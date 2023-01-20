using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class World : GameObject
{

    public World(Texture2D texture) : base(texture)
    {
        Position = new Vector2(0, 0);
    }
    
}