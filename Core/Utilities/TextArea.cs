using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DarkSoulsRogue.Core.Utilities;

public class TextArea
{
    
    private readonly RectangleBordered _area;
    private string _value;

    public TextArea(Rectangle area)
    {
        _area = new RectangleBordered(area, Colors.LightGray, Color.Black, 2);
        _value = "";
    }

    public void Reinit()
        => _value = "";

    public States Update()
    {
        if (Control.Enter.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuConfirm);
            return States.Confirmed;
        }
        if (Control.Backspace.IsOnePressed() && _value.Length > 0)
        {
            Sounds.Play(Sounds.SMenuBack);
            _value = _value?.Remove(_value.Length - 1);
        }
        _value += GetPressedCharacters();
        return States.Processing;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _area.Draw(spriteBatch);
        var pos = new Vector2(_area.Rectangle.X + _area.Thickness, _area.Rectangle.Y + _area.Thickness);
        spriteBatch.DrawString(Fonts.Font12, _value, pos, Colors.White);
    }

    public string Read() => _value;

    private static string GetPressedCharacters()
    {
        var result = "";
        var maj = Control.IsPressed(Keys.LeftShift) || Control.IsPressed(Keys.RightShift); 
        if (Control.A.IsOnePressed())
            result += maj ? "A" : "a";
        if (Control.B.IsOnePressed())
            result += maj ? "B" : "b";
        if (Control.C.IsOnePressed())
            result += maj ? "C" : "c";
        if (Control.D.IsOnePressed())
            result += maj ? "D" : "d";
        if (Control.E.IsOnePressed())
            result += maj ? "E" : "e";
        if (Control.F.IsOnePressed())
            result += maj ? "F" : "f";
        if (Control.G.IsOnePressed())
            result += maj ? "G" : "g";
        if (Control.H.IsOnePressed())
            result += maj ? "H" : "h";
        if (Control.I.IsOnePressed())
            result += maj ? "I" : "i";
        if (Control.J.IsOnePressed())
            result += maj ? "J" : "j";
        if (Control.K.IsOnePressed())
            result += maj ? "K" : "k";
        if (Control.L.IsOnePressed())
            result += maj ? "L" : "l";
        if (Control.M.IsOnePressed())
            result += maj ? "M" : "m";
        if (Control.N.IsOnePressed())
            result += maj ? "N" : "n";
        if (Control.O.IsOnePressed())
            result += maj ? "O" : "o";
        if (Control.P.IsOnePressed())
            result += maj ? "P" : "p";
        if (Control.Q.IsOnePressed())
            result += maj ? "Q" : "q";
        if (Control.R.IsOnePressed())
            result += maj ? "R" : "r";
        if (Control.S.IsOnePressed())
            result += maj ? "S" : "s";
        if (Control.T.IsOnePressed())
            result += maj ? "T" : "t";
        if (Control.U.IsOnePressed())
            result += maj ? "U" : "u";
        if (Control.V.IsOnePressed())
            result += maj ? "V" : "v";
        if (Control.W.IsOnePressed())
            result += maj ? "W" : "w";
        if (Control.X.IsOnePressed())
            result += maj ? "X" : "x";
        if (Control.Y.IsOnePressed())
            result += maj ? "Y" : "y";
        if (Control.Z.IsOnePressed())
            result += maj ? "Z" : "z";
        if (Control.D6.IsOnePressed())
            result += "-";
        if (Control.D8.IsOnePressed())
            result += "_";
        if (result != "")
            Sounds.Play(Sounds.SMenuMove);
        return result;
    }
    
    public enum States { Processing, Confirmed }

}