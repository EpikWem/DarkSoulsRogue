using System.Collections.Generic;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items;

public class Shield : Equipment
{

    public static readonly Shield
        NoShield = new (""),
        BasicShield = new ("Basic Shield");
    public static readonly List<Shield> Shields = new() {
        NoShield, BasicShield
    };
    public static Shield GetFromIndex(int i) => Shields[i];
    public static int GetIndexOf(Shield shield) => Shields.IndexOf(shield);
    
    public Texture2D GetTexture(Orientation orientation) => Textures.ShieldTextures[GetIndexOf(this)][(int)orientation];
    
    public Shield(string name) : base(name, Categories.Weapon)
    {
        
    }
    
}