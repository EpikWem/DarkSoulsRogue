using System.Threading;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Interfaces;

public static class BigMessage
{
    
    private const int PosY = Camera.Height / 2 - 12;

    private static bool _isActive;
    private static int _timer;
    private static string _msg;
    private static int _mrgX;
    private static Color _color;

    public static void Reset(string msg, int mrgX, Color color)
        => Reset(msg, mrgX, color, Sounds.SNewArea);
    public static void Reset(string msg, int mrgX, Color color, Sounds.Sfx sound)
    {
        _isActive = true;
        _timer = 60 * 5;
        _msg = msg;
        _mrgX = mrgX;
        _color = color;
        Sounds.Play(sound);
    }

    public static void Draw()
    {
        if (_timer == 0)
        {
            _isActive = false;
            return;
        }
        Main.SpriteBatch().DrawString(Fonts.FontHumanityCounter, "-" + _msg + "-", new Vector2(Camera.Width / 2 - _mrgX, PosY), _color);
        _timer--;
    }

    public static bool IsActive()
        => _isActive;
    
}