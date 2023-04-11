using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public abstract class Entity : GameObject
{
    private readonly Armor _baseArmor;
    public Orientation Orientation;
    private readonly Hitbox _hitbox;

    protected Entity(Armor baseArmor, Hitbox hitbox = null) : base(baseArmor.GetWearingTextures()[0])
    {
        _baseArmor = baseArmor;
        Orientation = Orientation.Down;
        _hitbox = hitbox ?? new Hitbox(Camera.CellSize, Camera.CellSize);
    }
    
    public Vector2 GetPosition() => Position;
    public Rectangle GetHitbox() => _hitbox.ToRectangle(this);
    public Rectangle GetGraphicbox() => new((int)Position.X, (int)Position.Y, Width, Height);

    public void SetPosition(int x, int y)
    {
        Position.X = x;
        Position.Y = y;
    }

    public int GetGWidth() => Width;
    public int GetGHeight() => Height;

    public void Draw(SpriteBatch batch, Armor equippedArmor = null)
    {
        //batch.Draw(Main.PixelTexture, GetGraphicbox(), Color.Orange);
        //batch.Draw(Main.PixelTexture, GetHitbox(), Color.Purple);
        batch.Draw((equippedArmor ?? _baseArmor).GetWearingTextures()[Orientation.Index], Position, Color.White);
        //batch.Draw(Main.PixelTexture, new Rectangle((int)GetLookingPoint().X, (int)GetLookingPoint().Y, 4, 4), Color.Red);
    }

    public Vector2 GetLookingPoint() => Orientation.Index switch {
            0 => new Vector2(Position.X + (float)Width/2, Position.Y),
            1 => new Vector2(Position.X + (float)Width/2, Position.Y + Height),
            2 => new Vector2(Position.X + Width, Position.Y + (float)Height/2),
            3 => new Vector2(Position.X, Position.Y + (float)Height/2),
            _ => new Vector2(0, 0) };

    public void PlaceOnGrid(int xGrid, int yGrid, Orientation orientation)
    {
        SetPosition(
            xGrid * Camera.CellSize,
            yGrid * Camera.CellSize - _hitbox.GetMarginY());
        Orientation = orientation;
    }
    
    public void PlaceOnGrid(Destination destination) => PlaceOnGrid((int)destination.PositionOnGrid.X, (int)destination.PositionOnGrid.Y, destination.Orientation);

    //TODO: public Vector2 PositionOnGrid() => new Vector2((float)Math.Truncate(Position.X / Camera.CellSize), (float)Math.Truncate(Position.Y / Camera.CellSize));
    
}