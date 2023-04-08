using DarkSoulsRogue.Core.GameObjects;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Utilities;

public class Hitbox
{

    private readonly int _marginX, _marginY;

    public Hitbox(int marginX, int marginY)
    {
        _marginX = marginX;
        _marginY = marginY;
    }

    public int GetMarginX() => _marginX;
    public int GetMarginY() => _marginY;

    public Rectangle ToRectangle(Entity entity) => new (
        (int)entity.GetPosition().X + _marginX, (int)entity.GetPosition().Y + _marginY,
         entity.GetWidth() - 2*_marginX, entity.GetHeight() - 2*_marginY);

}