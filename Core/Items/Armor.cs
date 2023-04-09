﻿using System.Collections.Generic;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items;

public class Armor : Equipment
{

    public static readonly Armor
        Naked = new ("", null, new[]{0f, 0f, 0f, 0f, 0, 0, 0}, new[]{0f, 0f, 0f}, 0f),
        Artorias = new ("Artorias", ArmorUpgrade.Unique, new[]{159.6f, 143.7f, 183.6f, 167.7f, 80.6f, 170.6f, 80.7f}, new[]{66f, 66f, 48f}, 24.8f, 37),
        //BlackIron = new ("Black Iron Armor"),
        Solaire = new ("Solaire", ArmorUpgrade.Unique, new[]{208f, 210.5f, 210.5f, 206.3f, 127f, 127f, 94f}, new[]{56f, 84f, 22.4f}, 23.1f, 40); 
    
    public static Armor GetFromIndex(int i) => Armors[i];
    public static int GetIndexOf(Armor armor) => Armors.IndexOf(armor);
    private static readonly List<Armor>
        Armors = new () {
            Naked,
            Artorias,
            //BlackIron,
            Solaire
        };

    public readonly float
        DefPhysical, DefStrike, DefSlash, DefThrust, DefMagic, DefFire, DefLightning,
        ResBlood, ResPoison, ResCurse, Weight;

    public readonly int Stability;

    private readonly ArmorUpgrade _upgradeType;
    private int _upgradeLevel;
    
    private Armor(
        string name, ArmorUpgrade upgradeType, float[] defs, float[] ress, float weight, int stability = 0)
        : base(name, Categories.Armor)
    {
        _upgradeType = upgradeType; _upgradeLevel = 0;
        DefPhysical = defs[0]; DefStrike = defs[1]; DefSlash = defs[2]; DefThrust = defs[3];
        DefMagic = defs[4]; DefFire = defs[5]; DefLightning = defs[6];
        ResBlood = ress[0]; ResPoison = ress[1]; ResCurse = ress[2];
        Weight = weight; Stability = stability;
    }

    public void SetUpgrade(int level)
    {
        _upgradeLevel = level;
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