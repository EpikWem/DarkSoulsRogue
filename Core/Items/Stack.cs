namespace DarkSoulsRogue.Core.Items;

public class Stack
{

    public Item Item;
    public int Quantity;

    public Stack(Item item)
    {
        Item = item;
        Quantity = 1;
    }
    public Stack(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

}