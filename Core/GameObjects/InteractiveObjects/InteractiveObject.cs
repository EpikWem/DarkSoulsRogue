using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public abstract class InteractiveObject : Wall
{
    protected int State;
    private Texture2D[] _textures;

    protected InteractiveObject(int xInGrid, int yInGrid, Texture2D[] textures) : base(xInGrid, yInGrid, textures[0])
    {
        State = 0;
        Position.X = xInGrid*Main.CellSize;
        Position.Y = yInGrid*Main.CellSize;
        _textures = textures;
    }

    public abstract void Interact(Character character);

    protected void IncreaseState()
    {
        State++;
        SetTexture(_textures[State]);
    }

    public int GetState() => State;
    
}