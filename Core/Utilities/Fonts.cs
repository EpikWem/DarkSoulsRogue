using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class Fonts
{
    
    public static SpriteFont Font, FontBold, FontHumanityCounter, FontSoulCounter;

    public Fonts(ContentManager content)
    {
        Font = content.Load<SpriteFont>("fonts/font");
        FontBold = content.Load<SpriteFont>("fonts/font_bold");
        FontHumanityCounter = content.Load<SpriteFont>("fonts/humanity_counter");
        FontSoulCounter = content.Load<SpriteFont>("fonts/soul_counter");
    }

}