using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Statics;

public static class Fonts
{
    
    public static SpriteFont
        Font12, Font14, Font16, Font18, Font24,
        FontBold12, FontBold14, FontBold16, FontBold18, FontBold24,
        FontHumanityCounter, FontSoulCounter;

    public static void Init(ContentManager content)
    {
        Font12 = LoadFont(content, "font12");
        Font14 = LoadFont(content, "font14");
        Font16 = LoadFont(content, "font16");
        Font18 = LoadFont(content, "font18");
        Font24 = LoadFont(content, "font24");
        FontBold12 = LoadFont(content, "font_bold12");
        FontBold14 = LoadFont(content, "font_bold14");
        FontBold16 = LoadFont(content, "font_bold16");
        FontBold18 = LoadFont(content, "font_bold18");
        FontBold24 = LoadFont(content, "font_bold24");
        FontHumanityCounter = LoadFont(content, "humanity_counter");
        FontSoulCounter = LoadFont(content, "soul_counter");
    }
    
    private static SpriteFont LoadFont(ContentManager content, string fontName) => content.Load<SpriteFont>("fonts/" + fontName);

}