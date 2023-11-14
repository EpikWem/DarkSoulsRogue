using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public class Ellipse
{

    public Vector2 Center { get; set; }
    public int RadiusX { get; set; }
    public int RadiusY { get; set; }

    public Ellipse(Vector2 center, int radiusX, int radiusY)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
    }

    public bool Contains(Vector2 point)
    {
        var normalized = new Vector2(point.X - Center.X, point.Y - Center.Y);
        return
            (double)(normalized.X * normalized.X)
            / (RadiusX * RadiusX) + (double)(normalized.Y * normalized.Y) / (RadiusY * RadiusY)
           <= 1.0;
        //Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2) <= Math.Pow(Radius, 2);
    }

    /*public bool Intersects(Rectangle rectangle)
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
    }*/

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        for(var y = (int)Center.Y - RadiusY; y < (int)Center.Y + RadiusY; y++)
            for(var x = (int)Center.X - RadiusX; x < (int)Center.X + RadiusX; x++)
                if (Contains(new Vector2(x, y)))
                    spriteBatch.Draw(Main.PixelTexture(), new Rectangle(x, y, 1, 1), color);
    }

}