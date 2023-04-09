using DarkSoulsRogue.Core.Statics;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Utilities;

public class Destination
{

    public readonly int MapId;
    public readonly Vector2 PositionOnGrid;
    public readonly Orientation Orientation;

    public Destination(int mapId, Vector2 positionOnGrid, Orientation orientation)
    {
        MapId = mapId;
        PositionOnGrid = positionOnGrid;
        Orientation = orientation;
    }
    
}