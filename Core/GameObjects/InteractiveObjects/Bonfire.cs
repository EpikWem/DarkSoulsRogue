using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{

    public new static string Name = "bonfire";
    public new static int StateNumber = 5; 
    
    public Bonfire(Texture2D[] textures, int xInGrid, int yInGrid) : base(textures, xInGrid, yInGrid)
    {
        
    }

    public override void Interact(Character character)
    {
        character.HealMax();
        Lit();
    }

    private void Lit()
    {
        if (State != 4)
            State++;
        UpdateTexture();
    }
    
}