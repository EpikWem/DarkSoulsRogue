namespace DarkSoulsRogue.Core.Items;

public class Catalyst
{

    public const int MaxUpgrade = 10; 
    
    public enum SpellCategories { Sorcery, Pyromancy, Miracle }
    public readonly SpellCategories SpellCategory;
    public readonly Mineral UpgradeMineral;
        
    private int _upgradeLevel;

}