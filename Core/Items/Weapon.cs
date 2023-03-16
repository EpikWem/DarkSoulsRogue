using System.Collections.Generic;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items;

public class Weapon : Equipment
{
    public static readonly Weapon
        Claymore = new ("Claymore", WeaponTypes.GreatSword, DamageTypes.Slash, true, new []{103, 0, 0, 0}, new ParamBonus(ParamBonus.C, ParamBonus.C, ParamBonus.F, ParamBonus.F), new float[]{60, 10, 40, 40}, 38, 6.0f),
        AstoraSword = new ("Astora's Straight Sword", WeaponTypes.StraightSword, DamageTypes.Slash, false, new []{80, 80, 0, 0}, new ParamBonus(ParamBonus.C, ParamBonus.C, ParamBonus.F, ParamBonus.C), new float[]{50, 10, 35, 35}, 32, 3.0f);
    
    public static Weapon GetFromIndex(int i) => Values[i];
    public static int GetIndexOf(Weapon weapon) => Values.IndexOf(weapon);
    private static readonly List<Weapon> Values =
        new () {
            Claymore,
            AstoraSword
        };
    
    
    
    public enum WeaponTypes
    {
        Dagger, StraightSword, GreatSword, UltraGreatSword, CurvedSword, Katana, CurvedGreatSword, PiercingSword, Axe,
        GreatAxe, Hammer, GreatHammer, Fist, Spear, Halberd, Whip
    }
    public readonly WeaponTypes WeaponType;
    
    public enum DamageTypes { Strike, Slash, Thrust }
    public readonly DamageTypes DamageType;

    public readonly bool Enchantable;

    private WeaponUpgrade _upgrade;
    private int _upgradeLevel;

    public readonly int
        AtkPhysical, AtkMagic, AtkFire, AtkLightning;

    public readonly ParamBonus ParamBonus;
    
    public readonly float
        DefPhysical, DefStrike, DefSlash, DefThrust,
        DefMagic, DefFire, DefLightning,
        ResBlood, ResPoison, ResCurse,
        Poise, Weight;

    public Weapon(
        string name, WeaponTypes weaponType, DamageTypes damageType, bool enchantable,
        int[] atks, ParamBonus paramBonus, float[] defs, float poise, float weight)
        : base(name, Categories.Weapon)
    {
        WeaponType = weaponType;
        DamageType = damageType;
        Enchantable = enchantable;
        _upgradeLevel = 0;
        AtkPhysical = atks[0]; AtkMagic = atks[1]; AtkFire = atks[2]; AtkLightning = atks[3];
        ParamBonus = paramBonus;
        DefPhysical = defs[0]; DefStrike = defs[1]; DefSlash = defs[2]; DefThrust = defs[3];
        DefMagic = defs[4]; DefFire = defs[5]; DefLightning = defs[6];
        //ResBlood = ress[0]; ResPoison = ress[1]; ResCurse = ress[2];
        Poise = poise;
        Weight = weight;
    }

    public int GetAttackDamage(Attributes attributes)
        => AtkPhysical + ParamBonus.GetBonusPhysicalDamages(attributes, this) +
        AtkMagic + ParamBonus.GetBonusMagicDamages(attributes, this) +
        AtkFire + AtkLightning +
        + 2 * ParamBonus.GetBonusChaosDamages(attributes, this);
    
    public void Upgrade(Inventory inventory)
    {
        if (!IsUpgradable())
            return;
        if (inventory.GetQuantity(_upgrade.UpgradeMineral) < _upgrade.Quantities[_upgradeLevel])
            return;
        _upgradeLevel++;
        inventory.RemoveItem(_upgrade.UpgradeMineral, _upgrade.Quantities[_upgradeLevel]);
    }
    
    public void Modify(Inventory inventory, WeaponUpgrade enchant)
    {
        if (!IsModifiable(enchant))
            return;
        if (inventory.GetQuantity(enchant.UpgradeMineral) < enchant.Quantities[0])
            return;
        _upgradeLevel = 0;
        _upgrade = enchant;
        inventory.RemoveItem(enchant.UpgradeMineral, enchant.Quantities[0]);
    }

    public bool IsUpgradable() => _upgradeLevel < _upgrade.Quantities.Length;

    public bool IsModifiable(WeaponUpgrade enchant)
        => !IsUpgradable() && enchant.BaseWeaponUpgrade == _upgrade;

    public string GetFullName()
        => _upgrade.Name + " " + Name + " +" + GetUpgradeLevel();

    public int GetUpgradeLevel()
        => _upgradeLevel + _upgrade.GetBaseUpgradeNumber();

}