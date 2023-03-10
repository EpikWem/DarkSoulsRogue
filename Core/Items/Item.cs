using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Items;

public abstract class Item
{

    public readonly string Name;
    public readonly Texture2D Texture;
    public readonly Categories Category;
    public readonly int Price;
    public readonly string Description;
    
    public enum Categories { Consumable, Weapon, Tool, Armor, Ring, Mineral, Key, Amo }

    protected Item(string name, Categories category)
    {
        Name = name;
        Category = category;
    }

}