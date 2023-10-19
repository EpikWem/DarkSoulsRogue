using System;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class Ath
{

    private const float BarScaleReduction = 1f;
    
    private static Bar _life, _stamina;
    private static Character _character;

    public static void Init(Character character)
    {
        _life = new Bar(new Vector2(20, 20), Colors.DarkRed, 0.005f);
        _stamina = new Bar(new Vector2(20, 40), Colors.DarkGreen, 0.02f);
        _character = character;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _life.Draw(spriteBatch, _character.Life, _character.MaxLife());
        _stamina.Draw(spriteBatch, _character.Stamina, _character.MaxStamina());
        SoulCounter.Draw(spriteBatch, _character.Souls);
        HumanityCounter.Draw(spriteBatch);
    }

    private static class SoulCounter
    {

        private const int CellWidth = 18, Width = CellWidth*6, Height = 28, Margin = 8;
        private static readonly Vector2 Position = new(Camera.Width - Width - Margin, Camera.Height - Height - Margin);
        private static readonly Rectangle Rectangle = new((int)Position.X, (int)Position.Y, Width, Height);

        public static void Draw(SpriteBatch spriteBatch, int souls)
        {
            spriteBatch.Draw(Main.PixelTexture, Rectangle, Color.Black);
            spriteBatch.DrawString(Fonts.FontSoulCounter, souls.ToString(), new Vector2(Position.X+Margin, Position.Y), Color.White);
        }
        public static void Draw(SpriteBatch spriteBatch, string text)
        {
            spriteBatch.Draw(Main.PixelTexture, Rectangle, Color.Black);
            spriteBatch.DrawString(Fonts.FontSoulCounter, text, new Vector2(0, Position.Y), Color.White);
        }

        private static int XForDisplay(int souls)
        {
            var i = (souls == 0 ? 0 : Math.Truncate(Math.Log10(souls))) + 1;
            var r = (int)(Camera.Width - CellWidth - (i * CellWidth)/2);
            return r;
        }

    }

    private static class HumanityCounter
    {

        private const int Radius = 36;
        private const int Border = 8;
        private static readonly Circle
            CircleIn = new(new Vector2(Radius, Radius), Radius-Border),
            CircleOut = new(new Vector2(Radius, Radius), Radius);

        public static void Draw(SpriteBatch spriteBatch)
        {
            CircleOut.Draw(spriteBatch, GetHumanityColor());
            CircleIn.Draw(spriteBatch, new Color(50, 50, 50));
            spriteBatch.DrawString(
                Fonts.FontHumanityCounter, 
                _character.Attributes.Get(Attributes.Attribute.Humanity).ToString(), 
                new Vector2(Border*2, 6), 
                GetHumanityColor());
        }

    }

    private static Color GetHumanityColor()
    {
        return _character.IsHuman ? new Color(250, 250, 250) : new Color(150, 150, 150);
    }

}