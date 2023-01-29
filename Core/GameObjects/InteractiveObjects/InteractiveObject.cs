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

    protected abstract void Interaction(Character character);
    
    public new void Draw(SpriteBatch batch)
    {
        batch.Draw(_textures[State], Position, Color.White);
    }
    
}