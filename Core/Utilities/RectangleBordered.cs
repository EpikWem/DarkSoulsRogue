using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class RectangleBordered
{
    
    public Rectangle Rectangle;
    public readonly Color ColorBorder, ColorBackground;
    public readonly int Thickness;

    public RectangleBordered(Vector2 position, int width, int height, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    public RectangleBordered(int x, int y, int width, int height, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = new Rectangle(x, y, width, height);
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    public RectangleBordered(Rectangle rectangle, Color colorBorder, Color colorBackground, int thickness = 1)
    {
        Rectangle = rectangle;
        ColorBorder = colorBorder;
        ColorBackground = colorBackground;
        Thickness = thickness;
    }
    
    public void Draw()
    {
        Main.SpriteBatch.Draw(Main.PixelTexture, Rectangle, ColorBorder);
        Main.SpriteBatch.Draw(
            Main.PixelTexture,
            new Rectangle(
                Rectangle.X + Thickness,
                Rectangle.Y + Thickness,
                Rectangle.Width - 2*Thickness, 
                Rectangle.Height - 2*Thickness),
            ColorBackground);
    }

    public void SetPosition(int x, int y) => Rectangle.Location = new Point(x, y);
    public void SetPosition(Vector2 position) => SetPosition((int)position.X, (int)position.Y);

    public Vector2 GetPosition() => new(Rectangle.X, Rectangle.Y);

}