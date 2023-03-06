using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items;

public class Weapon : Equipment
{

    public enum WeaponTypes
    {
        Dagger, StraightSword, GreatSword, UltraGreatSword, CurvedSword, Katana, CurvedGreatSword, PiercingSword, Axe,
        GreatAxe, Hammer, GreatHammer, Fist, Spear, Halberd, Whip
    }
    public readonly WeaponTypes WeaponType;
    
    public enum DamageTypes { Strike, Slash, Thrust }
    public readonly DamageTypes DamageType;

    public readonly Mineral UpgradeMineral;
    public readonly bool Enchantable;

    public readonly int
        AtkPhysical, AtkMagic, AtkFire, AtkLightning;

    public readonly float
        DefPhysical, DefStrike, DefSlash, DefThrust, DefMagic, DefFire, DefLightning,
        Poise, ResBlood, ResPoison, ResCurse, Weight;

    public Weapon(string name, WeaponTypes weaponType) : base(name, Categories.Weapon)
    {
        WeaponType = weaponType;
    }
    public Weapon(string name, WeaponTypes weaponType, Upgrades.Upgrade upgrade) : base(name, Categories.Weapon, upgrade)
    {
        WeaponType = weaponType;
    }
    
}