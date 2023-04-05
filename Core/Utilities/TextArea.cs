using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace DarkSoulsRogue.Core.Utilities;

public class TextArea
{
    
    private readonly RectangleHollow _area;
    private string _value;

    public TextArea(Rectangle area)
    {
        _area = new RectangleHollow(area, Color.WhiteSmoke, Color.Black, 2);
        _value = "";
    }

    public States Update()
    {
        if (Controls.Interact.IsPressed)
            return States.Confirmed;
        _value += GetPressedCharacters();
        return States.Processing;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _area.Draw(spriteBatch);
        var pos = new Vector2(_area.Rectangle.X + _area.Thickness, _area.Rectangle.Y + _area.Thickness);
        spriteBatch.DrawString(Fonts.Font, _value, pos, Color.Black);
    }

    public string Read() => _value;

    private string GetPressedCharacters()
    {
        var result = "";
        var maj = Controls.IsPressed(Keys.LeftShift) || Controls.IsPressed(Keys.RightShift); 
        if (Controls.IsPressed(Keys.A))
            result += maj ? "A" : "a";
        if (Controls.IsPressed(Keys.B))
            result += maj ? "B" : "b";
        if (Controls.IsPressed(Keys.C))
            result += maj ? "C" : "c";
        if (Controls.IsPressed(Keys.D))
            result += maj ? "D" : "d";
        if (Controls.IsPressed(Keys.E))
            result += maj ? "E" : "e";
        if (Controls.IsPressed(Keys.F))
            result += maj ? "F" : "f";
        if (Controls.IsPressed(Keys.G))
            result += maj ? "G" : "g";
        if (Controls.IsPressed(Keys.H))
            result += maj ? "H" : "h";
        if (Controls.IsPressed(Keys.I))
            result += maj ? "I" : "i";
        if (Controls.IsPressed(Keys.J))
            result += maj ? "J" : "j";
        if (Controls.IsPressed(Keys.K))
            result += maj ? "K" : "k";
        if (Controls.IsPressed(Keys.L))
            result += maj ? "L" : "l";
        if (Controls.IsPressed(Keys.M))
            result += maj ? "M" : "m";
        if (Controls.IsPressed(Keys.N))
            result += maj ? "N" : "n";
        if (Controls.IsPressed(Keys.O))
            result += maj ? "O" : "o";
        if (Controls.IsPressed(Keys.P))
            result += maj ? "P" : "p";
        if (Controls.IsPressed(Keys.Q))
            result += maj ? "Q" : "q";
        if (Controls.IsPressed(Keys.R))
            result += maj ? "R" : "r";
        if (Controls.IsPressed(Keys.S))
            result += maj ? "S" : "s";
        if (Controls.IsPressed(Keys.T))
            result += maj ? "T" : "t";
        if (Controls.IsPressed(Keys.U))
            result += maj ? "U" : "u";
        if (Controls.IsPressed(Keys.V))
            result += maj ? "V" : "v";
        if (Controls.IsPressed(Keys.W))
            result += maj ? "W" : "w";
        if (Controls.IsPressed(Keys.X))
            result += maj ? "X" : "x";
        if (Controls.IsPressed(Keys.Y))
            result += maj ? "Y" : "y";
        if (Controls.IsPressed(Keys.Z))
            result += maj ? "Z" : "z";
        if (Controls.IsPressed(Keys.D6))
            result += "-";
        if (Controls.IsPressed(Keys.D8))
            result += "_";
        return result;
    }
    
    public enum States { Processing, Confirmed }

}