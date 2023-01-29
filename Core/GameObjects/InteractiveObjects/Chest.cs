using DarkSoulsRogue.Core.Items;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Chest : InteractiveObject
{
    
    public new static string Name = "chest";
    public new static int StateNumber = 2; 

    private readonly Item _item;
    
    public Chest(Texture2D[] textures, int xInGrid, int yInGrid, Item item) : base(textures, xInGrid, yInGrid)
    {
        _item = item;
    }

    protected override void Interaction(Character character)
    {
        if (State == 0)
        {
            //TODO: give _content to player
            State = 1;
        }
    }
    
}