using DarkSoulsRogue.Core.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.System;

public class Camera
{
    
    public const int GridWidth = 15, GridHeight = 10, CellSize = 64;
    public const int Width = GridWidth*CellSize, Height = GridHeight*CellSize;

    public static Vector2 GetOrigin(Character character) => new();

}