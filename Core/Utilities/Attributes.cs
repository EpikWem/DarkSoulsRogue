using System;
using System.Collections.Generic;

namespace DarkSoulsRogue.Core.Utilities;

public class Attributes
{
    
    public enum Attribute { Level, Vitality, Attunement, Endurance, Strength, Dexterity, Resistance, Intelligence, Faith, Humanity }

    public const int NumAttributes = 10;
    
    private readonly int[] _values = new int[NumAttributes];

    public Attributes()
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = 1;
    }

    public int[] GetAll() => _values;
    public int Get(Attribute attribute) => _values[(int)attribute];

    public void Set(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = values[i];
    }
    public void Set(List<int> values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = values[i];
    }
    
    public void Increase(Attribute attribute, int value)
    {
        _values[(int)attribute] += value;
    }
    
    public void Increase(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] += values[i];
    }

    public int SoulToLevelUp(int newLevel) => newLevel switch
        {
            2 => 673,
            3 => 690,
            4 => 707,
            5 => 724,
            6 => 741,
            7 => 758,
            8 => 775,
            9 => 793,
            10 => 811,
            11 => 829,
            >= 12 => (int)Math.Ceiling(0.02 * (newLevel ^ 3) + 3.06 * (newLevel ^ 2) + 105.6 * newLevel - 895),
            _ => 1
        };


    public static string GetName(int index)
        => index switch { 
            0 => "Level",
            1 => "Vitality",
            2 => "Attunement",
            3 => "Endurance",
            4 => "Strength",
            5 => "Dexterity",
            6 => "Resistance",
            7 => "Intelligence",
            8 => "Faith",
            9 => "Humanity",
            _ => "<UNKNOWN>"
        };

}