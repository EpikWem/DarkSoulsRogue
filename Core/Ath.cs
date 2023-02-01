using DarkSoulsRogue.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Ath
{

    private readonly Bar _life, _stamina;
    private readonly Character _character;

    public Ath(Character character, GraphicsDevice graphicsDevice)
    {
        _character = character;
        _life = new Bar(graphicsDevice, 400, new Vector2(20, 20), 20, Color.Red);
        _stamina = new Bar(graphicsDevice, 300, new Vector2(20, 40), 20, Color.DarkGreen);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _life.Draw(spriteBatch, (float)_character.Life/_character.MaxLife());
        _stamina.Draw(spriteBatch, (float)_character.Stamina/_character.MaxStamina());
    }

    private class Bar
    {
        private const int Margin = 4;
        
        private readonly int _maxValue, _thickness;
        private readonly Vector2 _position;
        private readonly Texture2D _texture;

        public Bar(GraphicsDevice graphicsDevice, int maxValue, Vector2 position, int thickness, Color color)
        {
            _maxValue = maxValue;
            _position = position;
            _thickness = thickness;
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { color });
        }

        public void Draw(SpriteBatch spriteBatch, float pourcentage)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _maxValue, _thickness), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X+Margin, (int)_position.Y+Margin,
                (int)(pourcentage * _maxValue) - 2*Margin, _thickness - 2*Margin), Color.White);
        }
    }
    
}