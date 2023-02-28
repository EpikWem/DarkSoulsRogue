using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public abstract class GameObject
{

    protected readonly int Width, Height;
    protected Vector2 Position;
    private Texture2D _texture;

    protected GameObject()
    {
        Width = Main.CellSize;
        Height = Main.CellSize;
    }
    
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

    public Vector2 GetPositionOnGrid()
    {
        return new Vector2((int)(Position.X / Main.CellSize), (int)(Position.Y / Main.CellSize));
    }

    protected void SetTexture(Texture2D texture)
    {
        _texture = texture;
    }

}