using System.Collections.Generic;

namespace DarkSoulsRogue.Core.Items;

public class Inventory
{
    
    public readonly Item[] MenuItems = new Item[5];
    public readonly Item[] QuickItems = new Item[5];
    public readonly Amo[] EquippedAmos = new Amo[2];

    public Armor EquippedArmor;
    public Weapon EquippedWeapon;
    public Shield EquippedShield;
    public Catalyst EquippedCatalyst;
    public Ring EquippedRing;
    public Covenant EquippedCovenant;

    private readonly List<Stack> _items = new();
    
    public void AddItem(Item item, int quantity = 1)
    {
        bool Predicate(Stack s) => s.Item.Equals(item);
        if (_items.Exists(Predicate))
            _items.Find(Predicate).Quantity += quantity;
        else
            _items.Add(new Stack(item, quantity));
    }
    
    public void RemoveItem(Item item, int quantity = 1)
    {
        bool Predicate(Stack s) => s.Item.Equals(item);
        if (_items.Exists(Predicate))
        {
            if (GetQuantity(item) == 0)
                _items.Remove(_items.Find(Predicate));
            else
                _items.Find(Predicate).Quantity -= quantity;
        }
    }
    
    public int GetQuantity(Item item)
    {
        bool Predicate(Stack s) => s.Item.Equals(item);
        return _items.Exists(Predicate) ? _items.Find(Predicate).Quantity : 0;
    }
    
}