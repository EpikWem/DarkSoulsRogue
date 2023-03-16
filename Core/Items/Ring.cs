using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Items;

public abstract class Ring : Equipment
{
    
    public static Ring GetFromIndex(int i) => Rings[i];
    public static int GetIndexOf(Ring ring) => Rings.IndexOf(ring);

    public static readonly Ring
        NoRingR = new NoRing(),
        TinyBeingR = new TinyBeing(),
        CloranthyR = new Cloranthy();
    
    private static readonly List<Ring> Rings =
        new () {
            NoRingR,
            TinyBeingR,
            CloranthyR
        };
    
    private class NoRing : Ring
    {
        public NoRing() : base("No Ring") {}
        public override void Effect(Character character) {}
    }
    
    private class TinyBeing : Ring
    {
        public TinyBeing() : base("Cloranthy Ring") {}
        public override void Effect(Character character)
        {
            character.CoefLifeMax += 0.05f;
        }
    }
    
    private class Cloranthy : Ring
    {
        public Cloranthy() : base("Cloranthy Ring") {}
        public override void Effect(Character character)
        {
            character.CoefStaminaGain += 0.2f;
        }
    }
    
    protected Ring(string name) : base(name, Categories.Ring)
    {
        
    }

    public abstract void Effect(Character character);

}