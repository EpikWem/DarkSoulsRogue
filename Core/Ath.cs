using DarkSoulsRogue.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Ath
{

    private readonly Bar _life, _stamina;
    private readonly SoulCounter _soulCounter;
    private readonly Character _character;

    public Ath(Character character, GraphicsDevice graphicsDevice)
    {
        _character = character;
        _life = new Bar(graphicsDevice, new Vector2(20, 20), 20, Color.Red);
        _stamina = new Bar(graphicsDevice, new Vector2(20, 40), 20, Color.DarkGreen);
        _soulCounter = new SoulCounter(graphicsDevice);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _life.Draw(spriteBatch, _character.Life, _character.MaxLife());
        _stamina.Draw(spriteBatch, _character.Stamina, _character.MaxStamina());
        SoulCounter.Draw(spriteBatch, _character.Souls);
    }

    private class Bar
    {
        
        private const int Margin = 4;
        
        private readonly int _thickness;
        private readonly Vector2 _position;
        private readonly Texture2D _texture;

        public Bar(GraphicsDevice graphicsDevice, Vector2 position, int thickness, Color color)
        {
            _position = position;
            _thickness = thickness;
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { color });
        }

        public void Draw(SpriteBatch spriteBatch, int value, int maxValue)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, maxValue, _thickness), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X+Margin, (int)_position.Y+Margin,
                value - 2*Margin, _thickness - 2*Margin), Color.White);
        }
        
    }

    private class SoulCounter
    {

        private const int Width = 64, Height = 24;
        private static Texture2D _blackTexture;
        private static readonly Vector2 Position = new (Main.Width - Width - 10, Main.Height - Height - 5);
        private static readonly Rectangle Rectangle = new ((int)Position.X, (int)Position.Y, Width, Height);

        public SoulCounter(GraphicsDevice graphicsDevice)
        {
            _blackTexture = new Texture2D(graphicsDevice, 1, 1);
            _blackTexture.SetData(new [] { Color.Black });
        }

        public static void Draw(SpriteBatch spriteBatch, int souls)
        {
            spriteBatch.Draw(_blackTexture, Position, Rectangle, Color.White);
            spriteBatch.DrawString(Main.FontBold, souls.ToString(), new Vector2(Position.X, Position.Y), Color.White);
        }
        
    }
    
}