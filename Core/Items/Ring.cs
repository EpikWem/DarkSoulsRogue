using System;
using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Items;

public class Ring : Equipment
{
    
    public static Ring GetFromIndex(int i) => Rings[i];
    public static int GetIndexOf(Ring ring) => Rings.IndexOf(ring);

    public static readonly Ring
        NoRing = new ("", ENoRing),
        TinyBeing = new ("Tiny Being's Ring", ETinyBeing),
        Cloranthy = new ("Cloranthy Ring", ECloranthy),
        Favor = new ("Ring of Favor and Protection", EFavor);
    
    private static readonly List<Ring> Rings =
        new () {
            NoRing,
            TinyBeing,
            Cloranthy,
            Favor
        };
    
    private static void ENoRing() {}
    private static void ETinyBeing() { Main.Character.CoefLifeMax += 0.05f; }
    private static void ECloranthy() { Main.Character.CoefStaminaGain += 0.2f; }
    private static void EFavor() { Main.Character.CoefLifeMax += 0.2f; Main.Character.CoefStaminaMax += 0.2f; }

    public readonly Action Effect;

    private Ring(string name, Action effect) : base(name, Categories.Ring)
    {
        Effect = effect;
    }

    /*private class NoRing : Ring
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

    public abstract void Effect(Character character);*/

}