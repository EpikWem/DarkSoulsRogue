using DarkSoulsRogue.Core.Items;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Chest : InteractiveObject
{
    
    public const string Name = "chest";
    public const int StateNumber = 2; 

    private readonly Item _item;
    
    public Chest(int xInGrid, int yInGrid, Item item) : base(xInGrid, yInGrid)
    {
        _item = item;
    }

    public override void Interact(Character character)
    {
        if (State == 0)
        {
            //TODO: give _content to player
            State = 1;
        }
    }
    
}