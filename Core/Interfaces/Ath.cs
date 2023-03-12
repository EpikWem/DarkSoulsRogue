using System;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public class Ath
{

    private const int BarScaleReduction = 10; 

    private readonly Texture2D _pixel; 
    
    private readonly Bar _life, _stamina;
    private readonly Character _character;

    public Ath(Character character, GraphicsDevice graphicsDevice)
    {
        _pixel = new Texture2D(graphicsDevice, 1, 1);
        _pixel.SetData(new [] { Color.White });
        
        _character = character;
        _life = new Bar(new Vector2(20, 20), Color. DarkRed);
        _stamina = new Bar(new Vector2(20, 40), Color.DarkGreen);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _life.Draw(spriteBatch, _pixel, _character.Life, _character.MaxLife());
        _stamina.Draw(spriteBatch, _pixel, _character.Stamina, _character.MaxStamina());
        SoulCounter.Draw(spriteBatch, _pixel, _character.Souls);
        HumanityCounter.Draw(spriteBatch, _pixel, _character);
    }

    private class Bar
    {

        private const int Margin = 4, Thickness = 24;
        
        private readonly Vector2 _position;
        private readonly Color _color;

        public Bar(Vector2 position, Color color)
        {
            _position = position;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, int value, int maxValue)
        {
            value /= BarScaleReduction;
            maxValue /= BarScaleReduction;
            spriteBatch.Draw(pixel, new Rectangle((int)_position.X+54, (int)_position.Y, maxValue, Thickness), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle((int)_position.X+Margin+54, (int)_position.Y+Margin,
                value - 2*Margin, Thickness - 2*Margin), _color);
        }
        
    }

    private static class SoulCounter
    {

        private const int CellWidth = 18, Width = CellWidth*6, Height = 28, Margin = 8;
        private static readonly Vector2 Position = new (Main.Width - Width - Margin, Main.Height - Height - Margin);
        private static readonly Rectangle Rectangle = new ((int)Position.X, (int)Position.Y, Width, Height);

        public static void Draw(SpriteBatch spriteBatch, Texture2D pixel, int souls)
        {
            spriteBatch.Draw(pixel, Rectangle, Color.Black);
            spriteBatch.DrawString(Main.FontSoulCounter, souls.ToString(), new Vector2(Position.X+Margin, Position.Y), Color.White);
        }

        private static int XForDisplay(int souls)
        {
            var i = (souls == 0 ? 0 : Math.Truncate(Math.Log10(souls))) + 1;
            var r = (int)(Main.Width - CellWidth - (i * CellWidth)/2);
            return r;
        }

    }

    private static class HumanityCounter
    {

        private const int Radius = 36;
        private const int Border = 8;
        private static readonly Circle
            CircleIn = new (new Vector2(Radius, Radius), Radius-Border),
            CircleOut = new (new Vector2(Radius, Radius), Radius);

        public static void Draw(SpriteBatch spriteBatch, Texture2D pixel, Character character)
        {
            CircleOut.Draw(spriteBatch, pixel, GetHumanityColor(character));
            CircleIn.Draw(spriteBatch, pixel, new Color(50, 50, 50));
            spriteBatch.DrawString(
                Main.FontHumanityCounter, 
                character.Attributes.Get(Attributes.Attribute.Humanity).ToString(), 
                new Vector2(Border*2, 6), 
                GetHumanityColor(character));
        }

    }

    private static Color GetHumanityColor(Character character)
    {
        return character.IsHuman ? new Color(250, 250, 250) : new Color(150, 150, 150);
    }

}