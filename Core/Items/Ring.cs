using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Items;

public abstract class Ring : Equipment
{
    
    public Ring(string name) : base(name, Categories.Ring)
    {
        
    }

    public abstract void Effect(Character character);

}