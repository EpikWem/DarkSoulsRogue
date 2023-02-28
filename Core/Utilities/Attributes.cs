using System;
using System.Linq;

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
        Set(Attribute.Attunement, 0);
        Set(Attribute.Humanity, 0);
    }

    public int Get(Attribute attribute)
    {
        return _values[(int)attribute];
    }

    private void Set(Attribute attribute, int value)
    {
        _values[(int)attribute] = value;
    }
    
    public void Increase(Attribute attribute, int value)
    {
        _values[(int)attribute] += value;
    }

    public void Increase(int[] values)
    {
        for (int i = 0; i < _values.Length; i++)
        {
            _values[i] += values[i];
        }
    }

    public int GetTotalLevel() => _values.Sum();

}