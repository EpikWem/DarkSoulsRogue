namespace DarkSoulsRogue.Core.Items.Equipments;

public class Catalyst : Equipment
{
    
    public static readonly Catalyst
        WitcherCatalyst = new ("Witcher Catalyst", Catalyst.SpellCategories.Sorcery),
        PyromancyFlame = new ("Pyromancy Flame", Catalyst.SpellCategories.Pyromancy),
        Talisman = new ("Talisman", Catalyst.SpellCategories.Miracle);
    
    public enum SpellCategories { Sorcery, Pyromancy, Miracle }
    public readonly SpellCategories SpellCategory;

    public Catalyst(string name, SpellCategories spellCategory) : base(name, Categories.Tool)
    {
        SpellCategory = spellCategory;
    }
}