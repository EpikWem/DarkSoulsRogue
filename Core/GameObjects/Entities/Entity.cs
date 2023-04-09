using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public abstract class Entity : GameObject
{
    
    protected readonly Texture2D[] Textures;
    public Orientation Orientation;
    private readonly Hitbox _hitbox;

    protected Entity(Texture2D[] texture, Hitbox hitbox) : base(texture[0])
    {
        Textures = texture;
        Orientation = Orientation.Down;
        _hitbox = hitbox;
    }
    
    public Vector2 GetPosition() => Position;
    protected new Rectangle GetHitbox() => _hitbox.ToRectangle(this);

    public void SetPosition(int x, int y)
    {
        Position.X = x;
        Position.Y = y;
    }

    public int GetWidth() => Textures[0].Width;
    public int GetHeight() => Textures[0].Height;

    protected new void Draw(SpriteBatch batch)
    {
        batch.Draw(GetTextureToDraw(), Position, Color.White);
    }

    private Texture2D GetTextureToDraw() => Textures[Orientation.Index];

    private Vector2 GetLookingPoint()
    {
        return Orientation.Index switch
        {
            0 => new Vector2(Position.X + (float)Width/2, Position.Y),
            1 => new Vector2(Position.X + (float)Width/2, Position.Y + Height),
            2 => new Vector2(Position.X + Width, Position.Y + (float)Height/2),
            3 => new Vector2(Position.X, Position.Y + (float)Height/2),
            _ => new Vector2(0, 0)
        };
    }
    
    public Vector2 GetLookingCell()
    {
        return new Vector2(
            (int)(GetLookingPoint().X / Camera.CellSize),
            (int)(GetLookingPoint().Y / Camera.CellSize));
    }

    public void PlaceOnGrid(float xGrid, float yGrid, Orientation orientation)
    {
        Position.X = xGrid * Camera.CellSize + (float)_hitbox.GetMarginX()/2;
        Position.Y = yGrid * Camera.CellSize - _hitbox.GetMarginY();
        Orientation = orientation;
    }
    
    public void PlaceOnGrid(Destination destination)
    {
        PlaceOnGrid(destination.PositionOnGrid.X, destination.PositionOnGrid.Y, destination.Orientation);
    }

    //TODO: public Vector2 PositionOnGrid() => new Vector2((float)Math.Truncate(Position.X / Camera.CellSize), (float)Math.Truncate(Position.Y / Camera.CellSize));
    
}