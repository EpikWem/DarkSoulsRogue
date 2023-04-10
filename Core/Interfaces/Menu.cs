using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public abstract class Menu
{

    internal abstract void Reinit();
    internal abstract void Update();
    internal abstract void Draw(SpriteBatch spriteBatch);

}