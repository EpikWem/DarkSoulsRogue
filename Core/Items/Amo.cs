namespace DarkSoulsRogue.Core.Items;

public abstract class Amo : Item
{

    public readonly int Damages;
    
    public enum DamageTypes { Physical, Magic, Fire, Lightning }
    public readonly DamageTypes DamageType;
    
    protected Amo(string name, int damages, DamageTypes damageType) : base(name, Categories.Amo)
    {
        Damages = damages;
        DamageType = damageType;
    }
    
}