using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items;

public class Armor : Item
{

    public readonly float
        DefPhysical, DefStrike, DefSlash, DefThrust, DefMagic, DefFire, DefLightning,
        Poise, ResBlood, ResPoison, ResCurse, Weight;

    public readonly Texture2D WearingTexture;

    public Armor(string name) : base(name, Categories.Armor)
    {
        
    }
    
}