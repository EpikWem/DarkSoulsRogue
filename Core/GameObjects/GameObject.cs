using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public abstract class GameObject
{

    protected readonly int Width, Height;
    protected Vector2 Position;
    private Texture2D _texture;
    
    protected GameObject(Texture2D texture)
    {
        _texture = texture;
        Width = texture.Width;
        Height = texture.Height;
    }
    
    protected void Draw(SpriteBatch batch)
    {
        batch.Draw(_texture, Position, Color.White);
    }

    public Vector2 GetPositionOnGrid()
    {
        return new Vector2((int)(Position.X / Camera.CellSize), (int)(Position.Y / Camera.CellSize));
    }

    protected void SetTexture(Texture2D texture)
    {
        _texture = texture;
    }

}