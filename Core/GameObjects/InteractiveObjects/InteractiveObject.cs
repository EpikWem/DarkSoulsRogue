using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public abstract class InteractiveObject : Wall
{
    protected int State;
    private Texture2D[] _textures;

    protected InteractiveObject(int xInGrid, int yInGrid)
    {
        State = 0;
        Position.X = xInGrid*Main.CellSize;
        Position.Y = yInGrid*Main.CellSize;
    }

    public abstract void Interact(Character character);

    public void SetTextures(Texture2D[] textures)
    {
        _textures = textures;
        UpdateTexture();
    }

    public void IncreaseState()
    {
        State++;
        UpdateTexture();
    }
    
    private void UpdateTexture()
    {
        SetTexture(_textures[State]);
    }

    public int GetState() => State;
    
}