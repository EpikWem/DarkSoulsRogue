using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Character : GameObject
{

    private const int MarginS = 4, MarginU = 12, MarginD = 6;
    private const int Speed = 4;
    private Orientation _orientation;
    
    public Character(Texture2D texture) : base(texture)
    {
        Position = new Vector2(GameMain.CellSize*7, GameMain.CellSize*4);
        _orientation = Orientation.Up;
    }

    public void Move(Orientation orientation, List<Wall> walls)
    {
        Vector2 oldPosition = new Vector2(Position.X, Position.Y);
        _orientation = orientation;
        switch (_orientation) {
            case Orientation.Up: Position.Y -= Speed; break;
            case Orientation.Down: Position.Y += Speed; break;
            case Orientation.Right: Position.X += Speed; break;
            case Orientation.Left: Position.X -= Speed; break;
        }
        foreach (Wall w in walls)
        {
            if (w.GetHitbox().Intersects(GetHitbox()))
            {
                Position.X = oldPosition.X;
                Position.Y = oldPosition.Y;
            }
                
        }
    }

    private new Rectangle GetHitbox()
    {
        return new Rectangle((int)Position.X + MarginS, (int)Position.Y + MarginU, Width - MarginS*2, Height - MarginU - MarginD);
    }

}