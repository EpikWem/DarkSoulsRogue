using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public abstract class GameObject
{

    public readonly int Width, Height;
    public Vector2 Position;
    public Texture2D Texture;

    public GameObject(Texture2D texture)
    {
        Texture = texture;
        Width = texture.Width;
        Height = texture.Height;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Position, Color.White);
    }

    public Rectangle GetHitbox()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }

}