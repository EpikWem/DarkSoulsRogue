using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core;

public abstract class Maps
{
    
    public class Map
    {
        public readonly int Id;
        public readonly int[][] WallsIds;
        public readonly List<InteractiveObject> Objects;
        public readonly int Width, Height;

        public Map(int id, int[][] walls, List<InteractiveObject> objects)
        {
            Id = id;
            WallsIds = walls;
            Objects = objects;
            Width = WallsIds[0].Length;
            Height = WallsIds.Length;
        }
        
    }

    private static readonly Map
        UndeadAsylum1 = new (101,
            new[] {
                new[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            }, new List<InteractiveObject> (new InteractiveObject[] {
                new Door(7, 2, new Destination(102, new Vector2(7, 8), Orientation.Up))
            })
        ),
        UndeadAsylum2 = new (102,
            new[] {
                new[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 }
            }, new List<InteractiveObject> (new InteractiveObject[] {
                new Door(7, 0, new Destination(103, new Vector2(7, 8), Orientation.Up)),
                new Door(7, 9, new Destination(101, new Vector2(7, 3), Orientation.Down))
            })
        ),
        UndeadAsylum3 = new ( 103,
            new[] {
                new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1}
            }, new List<InteractiveObject> (new InteractiveObject[] {
                new Bonfire(12, 7),
                new Chest(12, 3, new Stack(Consumable.SoulOfALostUndead, 2)),
                new Door(7, 9, new Destination(102, new Vector2(7, 1), Orientation.Down))
            })
        );

    private static readonly Map[] MapArray = { UndeadAsylum1, UndeadAsylum2, UndeadAsylum3 };

    public static int GetObjectIdOf(InteractiveObject obj)
    {
        List<InteractiveObject> objArray = new ();
        foreach (var map in MapArray)
            objArray.AddRange(map.Objects);
        for (var i = 0; i < objArray.Count; i++)
            if (objArray[i] == obj)
                return i;
        return -1;
    }

    public static InteractiveObject GetObjectFromId(int id)
    {
        List<InteractiveObject> objArray = new ();
        foreach (var map in MapArray)
            objArray.AddRange(map.Objects);
        return objArray[id];
    }

    public static Map GetMap(int id)
    {
        return MapArray.First(m => m.Id == id);
    }

}