namespace DarkSoulsRogue.Core.Items;

public class Amo : Item
{
    
    public enum AmoTypes { Arrow, GreatArrow, Caret }

    public readonly AmoTypes AmoType;
    
    public Amo(string name, AmoTypes amoType) : base(name, Categories.Tool)
    {
        AmoType = amoType;
    }
    
}