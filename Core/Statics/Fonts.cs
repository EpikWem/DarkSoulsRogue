using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Statics;

public static class Fonts
{
    
    public static SpriteFont Font12, Font18, FontBold12, FontBold18, FontHumanityCounter, FontSoulCounter;

    public static void Init(ContentManager content)
    {
        Font12 = LoadFont(content, "font12");
        Font18 = LoadFont(content, "font18");
        FontBold12 = LoadFont(content, "font_bold12");
        FontBold18 = LoadFont(content, "font_bold18");
        FontHumanityCounter = LoadFont(content, "humanity_counter");
        FontSoulCounter = LoadFont(content, "soul_counter");
    }
    
    private static SpriteFont LoadFont(ContentManager content, string fontName) => content.Load<SpriteFont>("fonts/" + fontName);

}