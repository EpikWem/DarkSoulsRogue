using System;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Ath
{

    private readonly Bar _life, _stamina;
    private readonly SoulCounter _soulCounter;
    private readonly Character _character;
    private readonly Circle _humanityCounter;

    public Ath(Character character, GraphicsDevice graphicsDevice)
    {
        _character = character;
        _life = new Bar(graphicsDevice, new Vector2(20, 20), 20, Color.Red);
        _stamina = new Bar(graphicsDevice, new Vector2(20, 40), 20, Color.DarkGreen);
        _soulCounter = new SoulCounter(graphicsDevice);
        _humanityCounter = new Circle(new Vector2(100, 100));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _life.Draw(spriteBatch, _character.Life, _character.MaxLife());
        _stamina.Draw(spriteBatch, _character.Stamina, _character.MaxStamina());
        _soulCounter.Draw(spriteBatch, _character.Souls);
        spriteBatch.
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
            _texture.SetData(new [] { color });
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

        private const int CellWidth = 18, Width = CellWidth*6, Height = 28, Margin = 8;
        private static Texture2D _blackTexture;
        private static readonly Vector2 Position = new (Main.Width - Width - Margin, Main.Height - Height - Margin);
        private static readonly Rectangle Rectangle = new ((int)Position.X, (int)Position.Y, Width, Height);

        public SoulCounter(GraphicsDevice graphicsDevice)
        {
            _blackTexture = new Texture2D(graphicsDevice, 1, 1);
            _blackTexture.SetData(new [] { Color.Black });
        }

        public void Draw(SpriteBatch spriteBatch, int souls)
        {
            spriteBatch.Draw(_blackTexture, Rectangle, Color.Black);
            spriteBatch.DrawString(Main.FontSoulCounter, souls.ToString(), new Vector2(Position.X+Margin, Position.Y), Color.White);
        }

        private static int XForDisplay(int souls)
        {
            var i = (souls == 0 ? 0 : Math.Truncate(Math.Log10(souls))) + 1;
            var r = (int)(Main.Width - CellWidth - (i * CellWidth)/2);
            return r;
        }

    }
    
}