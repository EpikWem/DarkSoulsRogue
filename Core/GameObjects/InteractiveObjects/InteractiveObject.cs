using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public abstract class InteractiveObject : Wall
{
    protected int State;
    private readonly Texture2D[] _textures;

    protected InteractiveObject(int xInGrid, int yInGrid, Texture2D[] textures) : base(xInGrid, yInGrid, textures[0])
    {
        State = 0;
        Position.X = xInGrid*Camera.CellSize;
        Position.Y = yInGrid*Camera.CellSize;
        _textures = textures;
    }

    public abstract void Interact(Character character);

    protected void IncreaseState()
    {
        State++;
        UpdateTexture();
    }

    public int GetState() => State;

    public void SetState(int i)
    {
        State = i;
        UpdateTexture();
    }

    private void UpdateTexture()
    {
        SetTexture(_textures[State]);
    }
    
}