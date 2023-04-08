using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Character : Entity
{

    //private const int MarginS = 4, MarginU = 12, MarginD = 6;
    private const int ExhaustionTime = 120, BaseSpeed = 3, BaseSpeedSprint = 5;
    private const int BaseStaminaLoss = -20, BaseStaminaGain = 45;

    public string Name;
    public int Life, Stamina, Souls;
    public bool IsHuman;
    public readonly Attributes Attributes;
    public readonly Triggers Triggers;
    public readonly Inventory Inventory;
    private int _exhaustingTime;
    public bool ShieldUp;

    public Character(string name) : base(Armor.Naked.GetWearingTextures(), new Hitbox(4, 9))
    {
        Name = name;
        Attributes = new Attributes();
        Triggers = new Triggers();
        Inventory = new Inventory();
        ChangeArmor(Armor.Naked);
        ChangeWeapon(Weapon.BareFist);
        ChangeRing(Ring.NoRing);
        Life = MaxLife();
        Stamina = MaxStamina();
        _exhaustingTime = 0;
        ShieldUp = false;
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
        if (ShieldUp && Orientation == Orientation.Up)
        {
            spriteBatch.Draw(Inventory.EquippedShield.GetTexture(Orientation), Position, Color.White);
            base.Draw(spriteBatch);
            return;
        }
        base.Draw(spriteBatch);
        if (ShieldUp)
            spriteBatch.Draw(Inventory.EquippedShield.GetTexture(Orientation), Position, Color.White);
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
            AddStamina(BaseStaminaGain + (Inventory.EquippedRing == Ring.Cloranthy ? 20 : 0));
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
        var dest = Main.CurrentMap.Connections[(int)orientation];
        PlaceOnGrid(dest);
        Main.LoadMap(dest.MapId);
    }

    public int MaxLife()
    {
        return (int)((500 + Attributes.Get(Attributes.Attribute.Vitality) * 15) * 100
            * (Inventory.EquippedRing == Ring.Favor ? 1.2f : 1.0f)
            * (Inventory.EquippedRing == Ring.TinyBeing ? 1.05f : 1.0f));
    }
    public int MaxStamina()
    {
        return (int)((80 + Attributes.Get(Attributes.Attribute.Endurance) * 2) * (Inventory.EquippedRing == Ring.Favor ? 1.2f : 1.0f)) * 100;
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

    public void ChangeArmor(Armor armor)
    {
        Inventory.EquippedArmor = armor;
        for (var i = 0; i < Textures.Length; i++)
            Textures[i] = armor.GetWearingTextures()[i];
    }
    public void ChangeWeapon(Weapon weapon)
    {
        Inventory.EquippedWeapon = weapon;
    }
    public void ChangeShield(Shield shield)
    {
        Inventory.EquippedShield = shield;
    }
    public void ChangeRing(Ring ring)
    {
        Inventory.EquippedRing = ring;
    }
    
    public static Orientation OrientationFromId(int id)
        => id switch {
                0 => Orientation.Up,
                1 => Orientation.Down,
                2 => Orientation.Right,
                3 => Orientation.Left,
                _ => Orientation.Null
            };

}