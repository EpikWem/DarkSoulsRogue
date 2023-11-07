using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Interfaces;

public class Notification
{
    private const int AreaWidth = 200, AreaHeight = 100;
    private const int SelectorWidth = 50, SelectorHeight = 24;
    private static readonly Rectangle Area =
        new((Camera.Width - AreaWidth)/2, (Camera.Height - AreaHeight)/2, AreaWidth, AreaHeight);
    private static readonly Rectangle Selector = new((Camera.Width - SelectorWidth)/2, (Camera.Height - SelectorHeight)/2, SelectorWidth, SelectorHeight);

    private static bool _isActive;
    private static string _msg;
    
    public static void Reset(string msg)
    {
        _isActive = true;
        _msg = msg;
    }

    public static void Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuConfirm);
            _isActive = false;
        }
    }

    public static void Draw()
    {
        Main.SpriteBatch.Draw(Main.PixelTexture, Area, Colors.Black);
        Main.SpriteBatch.DrawString(Fonts.Font14, _msg, new Vector2(Area.X+8, Area.Y+6), Colors.White);
        Main.SpriteBatch.Draw(Main.PixelTexture, Selector, Colors.Orange);
        Main.SpriteBatch.DrawString(Fonts.Font16, "OK", new Vector2(Selector.X+8, Selector.Y+6), Colors.White);
    }

    public static bool IsActive()
        => _isActive;

}