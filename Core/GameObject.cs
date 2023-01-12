using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class GameObject
{

    public Vector2 Position;
    public Texture2D Texture;

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Position, Color.White);
    }

}