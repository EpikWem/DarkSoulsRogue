﻿using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{
    internal new const string Name = "bonfire";
    internal new const int StateNumber = 5; 
    
    public Bonfire(Texture2D[] textures, int xInGrid, int yInGrid) : base(textures, xInGrid, yInGrid)
    {
        
    }

    public override void Interact(Character character)
    {
        character.AddLife(character.MaxLife());
        character.AddStamina(character.MaxStamina());
        Lit();
    }

    private void Lit()
    {
        if (State != 4)
            State++;
        UpdateTexture();
    }
    
}