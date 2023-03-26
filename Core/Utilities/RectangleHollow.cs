using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class RectangleHollow
{
    
    private readonly Rectangle _rectangle;
    private readonly Color _color;
    private readonly int _thickness;

    public RectangleHollow(Vector2 position, int width, int height, Color color, int thickness = 1)
    {
        _rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        _color = color;
        _thickness = thickness;
    }
    public RectangleHollow(int x, int y, int width, int height, Color color, int thickness = 1)
    {
        _rectangle = new Rectangle(x, y, width, height);
        _color = color;
        _thickness = thickness;
    }
    public RectangleHollow(Rectangle rectangle, Color color, int thickness = 1)
    {
        _rectangle = rectangle;
        _color = color;
        _thickness = thickness;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, _rectangle, _color);
        spriteBatch.Draw(
            Main.PixelTexture,
            new Rectangle(
                _rectangle.X + _thickness,
                _rectangle.Y + _thickness,
                _rectangle.Width - 2*_thickness, 
                _rectangle.Height - 2*_thickness),
            Color.Black);
    }
    
}