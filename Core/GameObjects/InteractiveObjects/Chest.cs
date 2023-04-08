using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Chest : InteractiveObject
{
    
    public const string Name = "chest";
    public const int StateNumber = 2;
    
    private readonly Stack _item;
    
    public Chest(int xInGrid, int yInGrid, Stack stack) : base(xInGrid, yInGrid, Textures.ChestT)
    {
        _item = stack;
    }

    public override void Interact(Character character)
    {
        if (State == 0)
        {
            character.Inventory.AddItem(_item.Item, _item.Quantity);
            IncreaseState();
        }
    }
    
}