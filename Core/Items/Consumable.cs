namespace DarkSoulsRogue.Core.Items;

public class Consumable : Item
{
    
    public static readonly Consumable
        SoulOfALostUndead = new("Soul of a Lost Undead");
    
    public Consumable(string name) : base(name, Categories.Consumable)
    {
        
    }
    
}