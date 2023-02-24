using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public abstract class InteractiveObject : Wall
{
    public string Name;
    public int StateNumber; 
    protected int State;
    protected Texture2D[] Textures;

    protected InteractiveObject(int xInGrid, int yInGrid) : base(null, xInGrid, yInGrid)
    {
        State = 0;
    }

    public abstract void Interact(Character character);

    public void SetTextures(Texture2D[] textures)
    {
        Textures = textures;
        UpdateTexture();
    }

    protected void UpdateTexture()
    {
        SetTexture(Textures[State]);
    }
    
}