using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Character : GameObject
{

    private const int Dimx = 60, Dimy = 80;
    private const int Speed = 10;
    private Orientation _orientation;
    
    public Character(Texture2D texture) : base(texture)
    {
        Position = new Vector2(250, 200);
        _orientation = Orientation.Up;
    }
    
    public enum Orientation { Up, Down, Right, Left }

    public void Move(Orientation orientation)
    {
        _orientation = orientation;
        switch (_orientation)
        {
            case Orientation.Up: Position.Y -= Speed; if (Position.Y < 0) Position.Y = 0; break;
            case Orientation.Down: Position.Y += Speed; if (Position.Y > GameMain.Height - Dimy) Position.Y = GameMain.Height - Dimy; break;
            case Orientation.Right: Position.X += Speed; if (Position.X > GameMain.Width - Dimx) Position.X = GameMain.Width - Dimx; break;
            case Orientation.Left: Position.X -= Speed; if (Position.X < 0) Position.X = 0; break;
        }
    }

}