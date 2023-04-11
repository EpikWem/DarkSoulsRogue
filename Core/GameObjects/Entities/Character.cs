 using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public class Character : Entity
{

    //private const int MarginS = 4, MarginU = 12, MarginD = 6;
    private const int ExhaustionTime = 120;
    private const int BaseStaminaLoss = -20, BaseStaminaGain = 45;

    public string Name;
    public int Life, Stamina, Souls;
    public bool IsHuman;
    public readonly Attributes Attributes;
    public readonly Triggers Triggers;
    public readonly Inventory Inventory;
    private int _exhaustingTime;
    private bool _running;
    public bool ShieldUp;

    public Character(string name) : base(Armor.Naked, new Hitbox(4, 20))
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
        _running = false;
        ShieldUp = false;
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
        if (ShieldUp && Orientation == Orientation.Up)
        {
            spriteBatch.Draw(Inventory.EquippedShield.GetTexture(Orientation), Position, Color.White);
            base.Draw(spriteBatch, Inventory.EquippedArmor);
            return;
        }
        base.Draw(spriteBatch, Inventory.EquippedArmor);
        if (ShieldUp)
            spriteBatch.Draw(Inventory.EquippedShield.GetTexture(Orientation), Position, Color.White);
    }

    public void Move(List<Rectangle> collisions)
    {
        var oldPosition = new Vector2(Position.X, Position.Y);
        _running = false;
        
        // Running and Stamina control
        if (Controls.Run.IsPressed
            && GetNumberOfPressedMovements() >= 1)
        {
            if (Stamina < 0)
                _exhaustingTime = ExhaustionTime;
            if (_exhaustingTime == 0)
            {
                _running = true;
                AddStamina(BaseStaminaLoss);
            }
            else
                _exhaustingTime--;
        }
        else
            AddStamina(StaminaGain());

        // Test if movement is needed
        if (!Controls.TestForMovementKey())
            return;
        
        var speed = MovementSpeed();

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
        foreach (var c in collisions)
            if (c.Intersects(GetHitbox()))
            {
                Position.Y = oldPosition.Y;
                break;
            }
        
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
        foreach (var c in collisions)
            if (c.Intersects(GetHitbox()))
            {
                Position.X = oldPosition.X;
                break;
            }
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
        var cp = new Vector2(Position.X + (float)Width/2, Position.Y + (float)Height/2);
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
        var dest = Main.CurrentMap().Connections[orientation.Index];
        PlaceOnGrid(dest);
        Main.LoadMap(dest.MapId);
    }

    public int MaxLife() => (int)((500 + Attributes.Get(Attributes.Attribute.Vitality) * 15) * 100
         * (Inventory.EquippedRing == Ring.Favor ? 1.2f : 1f)
         * (Inventory.EquippedRing == Ring.TinyBeing ? 1.05f : 1f));

    public int MaxStamina() => (int)((80 + Attributes.Get(Attributes.Attribute.Endurance) * 2) * 100
        * (Inventory.EquippedRing == Ring.Favor ? 1.2f : 1f));
    
    private int StaminaGain() => (int)((BaseStaminaGain
        * (EquipLoadRatio() > 0.5f ? 0.7f : 1f)
        + (Inventory.EquippedRing == Ring.Cloranthy ? 20f : 0f)
        + (Inventory.EquippedShield == Shield.GrassShield ? 10f : 0f))
        * (ShieldUp ? 0.2f : 1f));
    
    private float MaxEquipLoad() => 40f + Attributes.Get(Attributes.Attribute.Endurance)
        * (Inventory.EquippedRing == Ring.Havel ? 1.5f : 1f)
        * (Inventory.EquippedRing == Ring.Favor ? 1.2f : 1f);

    public float EquipLoadRatio() =>
        (Inventory.EquippedArmor.Weight + Inventory.EquippedWeapon.Weight) //TODO: +etc...
        / MaxEquipLoad();

    public int MovementSpeed() => (GetNumberOfPressedMovements() > 1 && EquipLoadRatio() <= 0.75f ? -1 : 0) + EquipLoadRatio() switch {
        <= 0.25f    =>  _running ? 4 : 3,
        <= 0.50f    =>  _running ? 3 : 2,
        <= 0.75f    =>  _running ? 2 : 1,
        _           =>  0 };

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
    
    public void AddSouls(int souls) => Souls += souls;

    public void ChangeArmor(Armor armor) => Inventory.EquippedArmor = armor;
    public void ChangeWeapon(Weapon weapon) => Inventory.EquippedWeapon = weapon;
    public void ChangeShield(Shield shield) => Inventory.EquippedShield = shield;
    public void ChangeRing(Ring ring) => Inventory.EquippedRing = ring;

}