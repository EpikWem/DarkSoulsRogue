using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Character : GameObject
{

    private const int MarginS = 4, MarginU = 12, MarginD = 6;
    private const int Speed = 4;
    private Orientation _orientation;
    private readonly Texture2D[] _textures;
    private readonly Attributes _attributes;
    public int Life, Stamina, Souls;

    
    
    public Character(Texture2D[] textures) : base(textures[0])
    {
        Position = new Vector2(Main.CellSize*7, Main.CellSize*4);
        _orientation = Orientation.Up;
        _textures = textures;
        _attributes = new Attributes();
        HealMax();
    }

    public void Move(Orientation orientation, List<Wall> walls, bool run)
    {
        Vector2 oldPosition = new Vector2(Position.X, Position.Y);
        _orientation = orientation;
        int speed = Speed * (run ? 2 : 1);
        switch (_orientation) {
            case Orientation.Up: Position.Y -= speed; break;
            case Orientation.Down: Position.Y += speed; break;
            case Orientation.Right: Position.X += speed; break;
            case Orientation.Left: Position.X -= speed; break;
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

    public new void Draw(SpriteBatch batch)
    {
        batch.Draw(GetTextureToDraw(), Position, Color.White);
    }

    private Texture2D GetTextureToDraw()
    {
        return _orientation switch
        {
            Orientation.Up => _textures[0],
            Orientation.Down => _textures[1],
            Orientation.Right => _textures[2],
            Orientation.Left => _textures[3],
            _ => _textures[0]
        };
    }

    private Vector2 GetLookingPoint()
    {
        return _orientation switch
        {
            Orientation.Up => new Vector2(Position.X + Width/2, Position.Y),
            Orientation.Down => new Vector2(Position.X + Width/2, Position.Y + Height),
            Orientation.Right => new Vector2(Position.X + Width, Position.Y + Height/2),
            Orientation.Left => new Vector2(Position.X, Position.Y + Height/2),
            _ => new Vector2(0, 0)
        };
    }
    
    public Vector2 GetLookingCell()
    {
        Vector2 v = new Vector2(
            (int)(GetLookingPoint().X / Main.CellSize),
            (int)(GetLookingPoint().Y / Main.CellSize));
        return v;
    }

    public Orientation TestOutOfMap()
    {
        Vector2 cp = new Vector2(Position.X + Width/2, Position.Y + Height/2);
        if (cp.Y < 0)
            return Orientation.Up;
        if (cp.Y > Main.Height-1)
            return Orientation.Down;
        if (cp.X > Main.Width-1)
            return Orientation.Right;
        if (cp.X < 0)
            return Orientation.Left;
        return Orientation.Null;
    }

    public void TransitMap(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.Up:
                Position.Y = Main.Height - Height; break;
            case Orientation.Down:
                Position.Y = 0; break;
            case Orientation.Right:
                Position.X = 0; break;
            case Orientation.Left:
                Position.X = Main.Width - Width; break;
            default:
                throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
        }
    }

    private int MaxLife()
    {
        return _attributes.Get(Attributes.Attribute.Vitality) * 10;
    }

    public void Heal(int hp)
    {
        Life += hp;
        if (Life > MaxLife())
            Life = MaxLife();
    }

    public void HealMax()
    {
        Life = MaxLife();
    }

}