using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class RectangleHollow
{
    
    public readonly Rectangle Rectangle;
    public readonly Color ColorBorder, ColorBackground;
    public readonly int Thickness;

    public RectangleHollow(Vector2 position, int width, int height, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    public RectangleHollow(int x, int y, int width, int height, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = new Rectangle(x, y, width, height);
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    public RectangleHollow(Rectangle rectangle, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = rectangle;
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, Rectangle, ColorBorder);
        spriteBatch.Draw(
            Main.PixelTexture,
            new Rectangle(
                Rectangle.X + Thickness,
                Rectangle.Y + Thickness,
                Rectangle.Width - 2*Thickness, 
                Rectangle.Height - 2*Thickness),
            ColorBackground);
    }

}