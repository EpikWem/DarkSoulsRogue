using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public abstract class GameObject
{

    public readonly int DimX, DimY;
    public Vector2 Position;
    public Texture2D Texture;

    public GameObject(Texture2D texture)
    {
        Texture = texture;
        DimX = texture.Width;
        DimY = texture.Height;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Position, Color.White);
    }

}