using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Character : GameObject
{

    private const int MarginX = 4, MarginY = 6;
    private const int Speed = 4;
    private Orientation _orientation;
    
    public Character(Texture2D texture) : base(texture)
    {
        Position = new Vector2(250, 200);
        _orientation = Orientation.Up;
    }
    
    public enum Orientation { Up, Down, Right, Left }

    public void Move(Orientation orientation, Wall[] walls)
    {
        _orientation = orientation;
        switch (_orientation) {
            case Orientation.Up: Position.Y -= Speed; if (Position.Y < 0) Position.Y = 0; break;
            case Orientation.Down: Position.Y += Speed; if (Position.Y > GameMain.Height - Height) Position.Y = GameMain.Height - Height; break;
            case Orientation.Right: Position.X += Speed; if (Position.X > GameMain.Width - Width) Position.X = GameMain.Width - Width; break;
            case Orientation.Left: Position.X -= Speed; if (Position.X < 0) Position.X = 0; break;
        }
        foreach (Wall w in walls)
        {
            if (w.GetHitbox().Intersects(GetHitbox()))
                Position = new Vector2(250, 200);
        }
    }

    private new Rectangle GetHitbox()
    {
        return new Rectangle((int)Position.X + MarginX, (int)Position.Y + MarginY, Width - MarginX*2, Height - MarginY*2);
    }

}