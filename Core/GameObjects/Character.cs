using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Character : GameObject
{

    private const int MarginS = 4, MarginU = 12, MarginD = 6;
    private const int ExhaustionTime = 120, BaseSpeed = 3, BaseSpeedSprint = 5;
    private const int BaseStaminaLoss = -5, BaseStaminaGain = 10;

    public float CoefLifeMax, CoefStaminaMax, CoefStaminaGain;

    private readonly Texture2D[] _textures;
    public Orientation Orientation;
    
    public string Name;
    public int Life, Stamina, Souls;
    public bool IsHuman;
    public readonly Attributes Attributes;
    public readonly Triggers Triggers;
    public readonly Inventory Inventory;
    private int _exhaustingTime;

    
    
    public Character() : base(Armor.Naked.GetWearingTextures()[0])
    {
        _textures = new Texture2D[4];
        Attributes = new Attributes();
        Triggers = new Triggers();
        Inventory = new Inventory();
        ChangeArmor(Armor.Naked);
        ChangeWeapon(Weapon.BareFist);
        ChangeRing(Ring.NoRing);
        Life = MaxLife();
        Stamina = MaxStamina();
        _exhaustingTime = 0;
        ResetCoef();
    }

    public void Move(List<Wall> walls)
    {
        var oldPosition = new Vector2(Position.X, Position.Y);
        var speed = BaseSpeed;
        
        // Running and Stamina control
        if (Controls.Run.IsPressed
            && GetNumberOfPressedMovements() >= 1)
        {
            if (Stamina < 0)
                _exhaustingTime = ExhaustionTime;
            if (_exhaustingTime == 0)
            {
                speed = BaseSpeedSprint;
                AddStamina(BaseStaminaLoss);
            }
            else
            {
                _exhaustingTime--;
            }
        }
        else
        {
            AddStamina((int)(BaseStaminaGain * CoefStaminaGain));
        }

        // double speed fix (when pressing two directions at once)
        if (GetNumberOfPressedMovements() >= 2)
            speed--;
        
        // Real Movement Vertical
        if (Controls.Up.IsPressed)
        {
            Orientation = Orientation.Up;
            Position.Y -= speed;
        }
        if (Controls.Down.IsPressed)
        {
            Orientation = Orientation.Down;
            Position.Y += speed;
        }
        foreach (var w in walls.Where(w => w.GetHitbox().Intersects(GetHitbox())))
            Position.Y = oldPosition.Y;
        
        // Real Movement Horizontal
        if (Controls.Right.IsPressed)
        {
            Orientation = Orientation.Right;
            Position.X += speed;
        }
        if (Controls.Left.IsPressed)
        {
            Orientation = Orientation.Left;
            Position.X -= speed;
        }
        foreach (var w in walls.Where(w => w.GetHitbox().Intersects(GetHitbox())))
            Position.X = oldPosition.X;
        
    }

    private static int GetNumberOfPressedMovements()
    {
        var result = 0;
        if (Controls.Up.IsPressed)
            result++;
        if (Controls.Down.IsPressed)
            result++;
        if (Controls.Right.IsPressed)
            result++;
        if (Controls.Left.IsPressed)
            result++;
        return result;
    }

    public Vector2 GetPosition() => Position;

    public void SetPosition(int x, int y)
    {
        Position.X = x;
        Position.Y = y;
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
        return Orientation switch
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
        return Orientation switch
        {
            Orientation.Up => new Vector2(Position.X + (float)Width/2, Position.Y),
            Orientation.Down => new Vector2(Position.X + (float)Width/2, Position.Y + Height),
            Orientation.Right => new Vector2(Position.X + Width, Position.Y + (float)Height/2),
            Orientation.Left => new Vector2(Position.X, Position.Y + (float)Height/2),
            _ => new Vector2(0, 0)
        };
    }
    
    public Vector2 GetLookingCell()
    {
        return new Vector2(
            (int)(GetLookingPoint().X / Camera.CellSize),
            (int)(GetLookingPoint().Y / Camera.CellSize));
    }

    public Orientation TestOutOfMap()
    {
        var cp = new Vector2(Position.X + Width/2, Position.Y + Height/2);
        if (cp.Y < 0)
            return Orientation.Up;
        if (cp.Y > Camera.Height-1)
            return Orientation.Down;
        if (cp.X > Camera.Width-1)
            return Orientation.Right;
        if (cp.X < 0)
            return Orientation.Left;
        return Orientation.Null;
    }

    public void TransitMap(Orientation orientation)
    {
        if (orientation == Orientation.Null)
            return;
        var dest = Main.CurrentMap.Connections[OrientationId(orientation)];
        PlaceOnGrid(dest);
        Main.LoadMap(dest.MapId);
    }

    public void PlaceOnGrid(float xGrid, float yGrid, Orientation orientation)
    {
        Position.X = xGrid * Camera.CellSize + (float)(MarginS)/2;
        Position.Y = yGrid * Camera.CellSize - MarginU;
        Orientation = orientation;
    }
    
    public void PlaceOnGrid(Destination destination)
    {
        PlaceOnGrid(destination.PositionOnGrid.X, destination.PositionOnGrid.Y, destination.Orientation);
    }

    //TODO: public Vector2 PositionOnGrid() => new Vector2((float)Math.Truncate(Position.X / Camera.CellSize), (float)Math.Truncate(Position.Y / Camera.CellSize));

    public int MaxLife()
    {
        return Attributes.Get(Attributes.Attribute.Vitality) * (int)(CoefLifeMax * 500.0f);
    }
    public int MaxStamina()
    {
        return Attributes.Get(Attributes.Attribute.Endurance) * (int)(CoefStaminaMax * 300.0f);
    }

    public void AddLife(int hp)
    {
        Life += hp;
        if (Life > MaxLife())
            Life = MaxLife();
    }

    public void AddStamina(int sp)
    {
        Stamina += sp;
        if (Stamina > MaxStamina())
            Stamina = MaxStamina();
    }
    
    public void AddSouls(int souls)
    {
        Souls += souls;
    }

    private void ResetCoef()
    {
        CoefLifeMax = 1.0f;
        CoefStaminaMax = 1.0f;
        CoefStaminaGain = 1.0f;
        if (Life > MaxLife())
            Life = MaxLife();
        if (Stamina > MaxStamina())
            Stamina = MaxStamina();
    }

    public void ChangeArmor(Armor armor)
    {
        Inventory.EquippedArmor = armor;
        for (var i = 0; i < _textures.Length; i++)
            _textures[i] = armor.GetWearingTextures()[i];
    }
    
    public void ChangeRing(Ring ring)
    {
        ResetCoef();
        Inventory.EquippedRing = ring;
        ring.Effect();
    }

    public void ChangeWeapon(Weapon weapon)
    {
        Inventory.EquippedWeapon = weapon;
    }

    public static int OrientationId(Orientation orientation)
        => orientation switch {
            Orientation.Up => 0,
            Orientation.Down => 1,
            Orientation.Right => 2,
            Orientation.Left => 3,
            _ => -1
        };
    public static Orientation OrientationFromId(int id)
        => id switch {
                0 => Orientation.Up,
                1 => Orientation.Down,
                2 => Orientation.Right,
                3 => Orientation.Left,
                _ => Orientation.Null
            };

}