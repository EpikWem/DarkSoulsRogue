namespace DarkSoulsRogue.Core.Items;

public class Catalyst : Equipment
{ 
    
    public enum SpellCategories { Sorcery, Pyromancy, Miracle }
    public readonly SpellCategories SpellCategory;

    public Catalyst(string name, SpellCategories spellCategory) : base(name, Categories.Tool)
    {
        SpellCategory = spellCategory;
    }
}