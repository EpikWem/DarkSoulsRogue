using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class Bar
{

    private const int Margin = 4, Thickness = 24;
        
    private readonly Vector2 _position;
    private readonly Color _color;
    private readonly float _scale;

    public Bar(Vector2 position, Color color, float scale)
    {
        _position = position;
        _color = color;
        _scale = scale;
    }

    public void Draw(int value, int maxValue)
    {
        value = (int)(value * _scale);
        maxValue = (int)(maxValue * _scale);
        Main.SpriteBatch.Draw(Main.PixelTexture, new Rectangle((int)_position.X+54, (int)_position.Y, maxValue, Thickness), Color.Black);
        Main.SpriteBatch.Draw(Main.PixelTexture, new Rectangle((int)_position.X+Margin+54, (int)_position.Y+Margin, value - 2*Margin, Thickness - 2*Margin), _color);
    }
        
}