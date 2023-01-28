using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public abstract class GameObject
{

    protected readonly int Width, Height;
    protected Vector2 Position;
    private readonly Texture2D _texture;

    protected GameObject(Texture2D texture)
    {
        _texture = texture;
        Width = texture.Width;
        Height = texture.Height;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(_texture, Position, Color.White);
    }

    public Rectangle GetHitbox()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }

}