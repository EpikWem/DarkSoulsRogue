using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class ChatBox
{

    private const int Width = (int)(Camera.Width * 0.7f);
    private const int Height = 32;
    private static readonly Vector2 Position = new(((float)Camera.Width - Width)/2, Camera.Height - Height - 4);

    private static string _text = "";

    public static void Say(string text) => _text = text;
    public static void Clear() => _text = "";

    public static void Draw() => Main.SpriteBatch.DrawString(Fonts.FontBold12, _text, Position, Color.WhiteSmoke);
}