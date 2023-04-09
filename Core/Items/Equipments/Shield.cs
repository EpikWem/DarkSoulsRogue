using System.Collections.Generic;
using DarkSoulsRogue.Core.Statics;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items.Equipments;

public class Shield : Equipment
{

    public static readonly Shield
        NoShield = new (""),
        BasicShield = new ("Basic Shield"),
        GrassShield = new ("Grass Crest Shield");
    private static readonly List<Shield> Shields = new() {
        NoShield, BasicShield, GrassShield
    };
    public static Shield GetFromIndex(int i) => Shields[i];
    public static int GetIndexOf(Shield shield) => Shields.IndexOf(shield);
    
    public Texture2D GetTexture(Orientation orientation) => Textures.ShieldTextures[GetIndexOf(this)][orientation.Index];

    private Shield(string name) : base(name, Categories.Weapon)
    {
        
    }
    
}