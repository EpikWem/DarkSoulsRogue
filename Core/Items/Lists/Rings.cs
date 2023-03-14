using System;
using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Items.Lists;

public static class Rings
{
    
    public static Ring GetFromIndex(int i) => Values[i];
    public static int GetIndexOf(Ring ring) => Values.IndexOf(ring);

    public static readonly Ring
        NoRingR = new NoRing(),
        TinyBeingR = new TinyBeing(),
        CloranthyR = new Cloranthy();
    
    private static readonly List<Ring> Values =
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
    
}