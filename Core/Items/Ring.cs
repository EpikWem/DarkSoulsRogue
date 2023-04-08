using System.Collections.Generic;

namespace DarkSoulsRogue.Core.Items;

public class Ring : Equipment
{
    
    public static Ring GetFromIndex(int i) => Rings[i];
    public static int GetIndexOf(Ring ring) => Rings.IndexOf(ring);

    public static readonly Ring
        NoRing = new (""),
        TinyBeing = new ("Tiny Being's Ring"),
        Cloranthy = new ("Cloranthy Ring"),
        Favor = new ("Ring of Favor and Protection");
    
    private static readonly List<Ring> Rings = new () {
        NoRing,
        TinyBeing,
        Cloranthy,
        Favor
    };

    private Ring(string name) : base(name, Categories.Ring)
    {
        
    }

}