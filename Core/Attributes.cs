using System;

namespace DarkSoulsRogue.Core;

public class Attributes
{
    
    public enum Attribute { Vitality, Endurance, Attunement, Strength, Dexterity, Resistance, Intelligence, Faith, Humanity }

    private int[] _values = new int[9]; 

    public Attributes()
    {
        for (int i = 0; i < _values.Length; i++)
        {
            _values[i] = 10;
        }
    }

    public int Get(Attribute attribute)
    {
        return _values[(int)attribute];
    }

    public void Set(Attribute attribute, int value)
    {
        _values[(int)attribute] = value;
    }

}