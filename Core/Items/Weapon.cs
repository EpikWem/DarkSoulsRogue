using System.Collections.Generic;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items;

public class Weapon : Equipment
{

    public static readonly Weapon
        BareFist = new Weapon("Bare Fist", WeaponTypes.Fist, DamageTypes.Strike, false, new []{10, 0, 0, 0}, new ParamBonus(ParamBonus.C, ParamBonus.C, ParamBonus.F, ParamBonus.F), new []{0f, 0f, 0f, 0f}, 0, 0),
        Claymore = new ("Claymore", WeaponTypes.GreatSword, DamageTypes.Slash, true, new []{103, 0, 0, 0}, new ParamBonus(ParamBonus.C, ParamBonus.C, ParamBonus.F, ParamBonus.F), new float[]{0.6f, 0.1f, 0.4f, 0.4f}, 38, 6.0f),
        AstoraSword = new ("Astoras Straight Sword", WeaponTypes.StraightSword, DamageTypes.Slash, false, new []{80, 80, 0, 0}, new ParamBonus(ParamBonus.C, ParamBonus.C, ParamBonus.F, ParamBonus.C), new float[]{0.5f, 0.1f, 0.35f, 0.35f}, 32, 3.0f);
    
    public static Weapon GetFromIndex(int i) => Weapons[i];
    public static int GetIndexOf(Weapon weapon) => Weapons.IndexOf(weapon);
    private static readonly List<Weapon>
        Weapons = new () {
            BareFist,
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
    
    public readonly float DefPhysical, DefMagic, DefFire, DefLightning, Weight;
    public readonly int Stability;

    public Weapon(string name) : base(name, Categories.Weapon)
    {
        
    }
    
    private Weapon(
        string name, WeaponTypes weaponType, DamageTypes damageType, bool enchantable,
        int[] atks, ParamBonus paramBonus, float[] defs, int stability, float weight)
        : base(name, Categories.Weapon)
    {
        WeaponType = weaponType;
        DamageType = damageType;
        Enchantable = enchantable;
        _upgradeLevel = 0;
        _upgrade = WeaponUpgrade.Regular;
        AtkPhysical = atks[0]; AtkMagic = atks[1]; AtkFire = atks[2]; AtkLightning = atks[3];
        ParamBonus = paramBonus;
        DefPhysical = defs[0]; DefMagic = defs[1]; DefFire = defs[2]; DefLightning = defs[3];
        Weight = weight;
        Stability = stability;
    }

    public void SetUpgrade(WeaponUpgrade enchant, int level)
    {
        _upgrade = enchant;
        _upgradeLevel = level;
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