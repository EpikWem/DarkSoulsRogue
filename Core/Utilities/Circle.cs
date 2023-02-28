using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class Circle
{

    public Vector2 Center { get; set; }
    public int Radius { get; set; }

    public Circle(Vector2 center, int radius)
    {
        Center = center;
        Radius = radius;
    }

    public bool Contains(Vector2 point) =>
        Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2) <= Math.Pow(Radius, 2);

    public bool Intersects(Rectangle rectangle)
    {
        var d = new Vector2();
        d.X = Math.Abs(Center.X - rectangle.Center.X);
        d.Y = Math.Abs(Center.Y - rectangle.Center.Y);

        if (d.X > rectangle.Width/2 + Radius) { return false; }
        if (d.Y > rectangle.Height/2 + Radius) { return false; }

        if (d.X <= (float)rectangle.Width/2) { return true; } 
        if (d.Y <= (float)rectangle.Height/2) { return true; }

        var cornerDistanceSq = (int)(d.X - (float)rectangle.Width/2)^2 +
            (int)(d.Y - (float)rectangle.Height/2)^2;

        return (cornerDistanceSq <= (Radius^2));
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel, Color color)
    {
        for(int y = (int)Center.Y-Radius; y < (int)Center.Y+Radius; y++)
            for(int x = (int)Center.X-Radius; x < (int)Center.X+Radius; x++)
                if (Contains(new Vector2(x, y)))
                    spriteBatch.Draw(pixel, new Rectangle(x, y, 1, 1), color);
    }

}