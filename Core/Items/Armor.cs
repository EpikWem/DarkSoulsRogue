using System.Collections.Generic;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items;

public class Armor : Equipment
{

    public static readonly Armor
        Naked = new ("", null),
        Artorias = new ("Artorias", ArmorUpgrade.Unique),
        //BlackIron = new ("Black Iron Armor"),
        Solaire = new ("Solaire", ArmorUpgrade.Unique); 
    
    public static Armor GetFromIndex(int i) => Armors[i];
    public static int GetIndexOf(Armor armor) => Armors.IndexOf(armor);
    private static readonly List<Armor> Armors =
        new () {
            Naked,
            Artorias,
            //BlackIron,
            Solaire
        };

    public readonly float
        DefPhysical, DefStrike, DefSlash, DefThrust, DefMagic, DefFire, DefLightning,
        Poise, ResBlood, ResPoison, ResCurse, Weight;

    private readonly ArmorUpgrade _upgradeType;
    private int _upgradeLevel;
    
    public Armor(string name, ArmorUpgrade upgradeType) : base(name, Categories.Armor)
    {
        _upgradeType = upgradeType;
        _upgradeLevel = 0;
    }

    public void Upgrade(Inventory inventory)
    {
        if (!IsUpgradable())
            return;
        if (inventory.GetQuantity(_upgradeType.UpgradeMineral) < _upgradeType.Quantities[_upgradeLevel])
            return;
        _upgradeLevel++;
        inventory.RemoveItem(_upgradeType.UpgradeMineral, _upgradeType.Quantities[_upgradeLevel]);
    }
    
    public bool IsUpgradable() => _upgradeLevel < _upgradeType.Quantities.Length;
    
    public Texture2D[] GetWearingTextures() => Textures.ArmorTextures[GetIndexOf(this)];

    public int GetUpgradeLevel() => _upgradeLevel;

}