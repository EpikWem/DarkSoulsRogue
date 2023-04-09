using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        if (Controls.Enter.IsOnePressed)
            return States.Confirmed;
        var v = GetPressedCharacters();
        if (v != "")
            _value += v;
        return States.Processing;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _area.Draw(spriteBatch);
        var pos = new Vector2(_area.Rectangle.X + _area.Thickness, _area.Rectangle.Y + _area.Thickness);
        spriteBatch.DrawString(Fonts.Font, _value, pos, Color.White);
    }

    public string Read() => _value;

    private static string GetPressedCharacters()
    {
        var result = "";
        var maj = Controls.IsPressed(Keys.LeftShift) || Controls.IsPressed(Keys.RightShift); 
        if (Controls.A.IsOnePressed)
            result += maj ? "A" : "a";
        if (Controls.B.IsOnePressed)
            result += maj ? "B" : "b";
        if (Controls.C.IsOnePressed)
            result += maj ? "C" : "c";
        if (Controls.D.IsOnePressed)
            result += maj ? "D" : "d";
        if (Controls.E.IsOnePressed)
            result += maj ? "E" : "e";
        if (Controls.F.IsOnePressed)
            result += maj ? "F" : "f";
        if (Controls.G.IsOnePressed)
            result += maj ? "G" : "g";
        if (Controls.H.IsOnePressed)
            result += maj ? "H" : "h";
        if (Controls.I.IsOnePressed)
            result += maj ? "I" : "i";
        if (Controls.J.IsOnePressed)
            result += maj ? "J" : "j";
        if (Controls.K.IsOnePressed)
            result += maj ? "K" : "k";
        if (Controls.L.IsOnePressed)
            result += maj ? "L" : "l";
        if (Controls.M.IsOnePressed)
            result += maj ? "M" : "m";
        if (Controls.N.IsOnePressed)
            result += maj ? "N" : "n";
        if (Controls.O.IsOnePressed)
            result += maj ? "O" : "o";
        if (Controls.P.IsOnePressed)
            result += maj ? "P" : "p";
        if (Controls.Q.IsOnePressed)
            result += maj ? "Q" : "q";
        if (Controls.R.IsOnePressed)
            result += maj ? "R" : "r";
        if (Controls.S.IsOnePressed)
            result += maj ? "S" : "s";
        if (Controls.T.IsOnePressed)
            result += maj ? "T" : "t";
        if (Controls.U.IsOnePressed)
            result += maj ? "U" : "u";
        if (Controls.V.IsOnePressed)
            result += maj ? "V" : "v";
        if (Controls.W.IsOnePressed)
            result += maj ? "W" : "w";
        if (Controls.X.IsOnePressed)
            result += maj ? "X" : "x";
        if (Controls.Y.IsOnePressed)
            result += maj ? "Y" : "y";
        if (Controls.Z.IsOnePressed)
            result += maj ? "Z" : "z";
        if (Controls.D6.IsOnePressed)
            result += "-";
        if (Controls.D8.IsOnePressed)
            result += "_";
        return result;
    }
    
    public enum States { Processing, Confirmed }

}