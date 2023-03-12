using DarkSoulsRogue.Core.Items;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Loot : InteractiveObject
{
    
    public const string Name = "loot";
    public const int StateNumber = 1;
    
    private readonly Stack _item;
    
    
    public Loot(int xInGrid, int yInGrid, Stack stack) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        _item = stack;
    }

    public override void Interact(Character character)
    {
        character.Inventory.AddItem(_item.Item, _item.Quantity);
    }
    
}