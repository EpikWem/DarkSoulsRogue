using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Utilities;

public class Destination
{

    public readonly int MapId;
    public readonly Vector2 PositionOnGrid;

    public Destination(int mapId, Vector2 positionOnGrid)
    {
        MapId = mapId;
        PositionOnGrid = positionOnGrid;
    }
    
}