using System.Linq;

namespace DarkSoulsRogue.Core.Utilities;

public class Attributes
{
    
    public enum Attribute { Vitality, Endurance, Attunement, Strength, Dexterity, Resistance, Intelligence, Faith, Humanity }

    public const int NumAttributes = 9;
    
    private readonly int[] _values = new int[NumAttributes]; 

    public Attributes()
    {
        for (int i = 0; i < NumAttributes; i++)
            _values[i] = 10;
        Set(Attribute.Attunement, 0);
        Set(Attribute.Humanity, 0);
    }

    public int[] GetAll() => _values;
    public int Get(Attribute attribute) => _values[(int)attribute];

    private void Set(Attribute attribute, int value)
    {
        _values[(int)attribute] = value;
    }
    
    public void Set(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
        {
            _values[i] = values[i];
        }
    }
    
    public void Increase(Attribute attribute, int value)
    {
        _values[(int)attribute] += value;
    }
    
    public void Increase(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
        {
            _values[i] += values[i];
        }
    }

    public int GetTotalLevel() => _values.Sum();

}