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
        Position = new Vector2(GameMain.CellSize*7, GameMain.CellSize*4);
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