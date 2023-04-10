using DarkSoulsRogue.Core.Statics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects;

public class Background : GameObject
{

    public Background() : base(Textures.BgT)
    {
        Position = new Vector2(0, 0);
    }

    public new void SetTexture(Texture2D texture) => base.SetTexture(texture);

    public new void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
    
}