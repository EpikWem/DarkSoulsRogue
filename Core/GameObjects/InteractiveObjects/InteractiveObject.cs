using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public abstract class InteractiveObject : Wall
{
    public static string Name;
    public static int StateNumber; 
    protected int State;
    private readonly Texture2D[] _textures;

    protected InteractiveObject(Texture2D[] textures, int xInGrid, int yInGrid) : base(textures[0], xInGrid, yInGrid)
    {
        State = 0;
        _textures = textures;
    }

    public abstract void Interact(Character character);

    protected void UpdateTexture()
    {
        SetTexture(_textures[State]);
    }
    
}