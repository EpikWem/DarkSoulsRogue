using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public static class Fonts
{
    
    public static SpriteFont Font, FontBold, FontHumanityCounter, FontSoulCounter;

    public static void Init(ContentManager content)
    {
        Font = LoadFont(content, "font");
        FontBold = LoadFont(content, "font_bold");
        FontHumanityCounter = LoadFont(content, "humanity_counter");
        FontSoulCounter = LoadFont(content, "soul_counter");
    }
    
    private static SpriteFont LoadFont(ContentManager content, string fontName) => content.Load<SpriteFont>("fonts/" + fontName);

}