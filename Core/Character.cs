using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Character : GameObject
{
    
    public Character(Texture2D texture)
    {
        Texture = texture;
        Position = new Vector2(250, 200);
    }

}