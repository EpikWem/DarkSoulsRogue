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
        public readonly Destination[] Connections;

        public Map(int id, int[][] walls, List<InteractiveObject> objects, Destination[] connections)
        {
            Id = id;
            WallsIds = walls;
            Objects = objects;
            Width = WallsIds[0].Length;
            Height = WallsIds.Length;
            Connections = connections;
        }
        
    }

    private static readonly Map
        UndeadAsylum1 = new (101,
            new[] {
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            }, new List<InteractiveObject>(new InteractiveObject[] {
                new LockedDoor(7, 2, Key.AsylumCell),
                new Loot(9, 6, new Stack(Key.AsylumCell))
            }), new Destination[] {
                new (102, new Vector2(7, 8), Orientation.Up),
                null,
                null,
                null
            }
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
            }, new List<InteractiveObject> (System.Array.Empty<InteractiveObject>()),
            new Destination[] {
                new (103, new Vector2(4, 9), Orientation.Up),
                new (101, new Vector2(7, 3), Orientation.Down), 
                null,
                null
            }
        ),
        UndeadAsylum3 = new ( 103,
            new[] {
                new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0},
                new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0},
                new[] {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0}
            }, new List<InteractiveObject> (new InteractiveObject[] {
                new Ladder(11, 1, false, new Destination(104, new Vector2(7, 7), Orientation.Up))
            }), new Destination[] {
                null,
                new (102, new Vector2(7, 1), Orientation.Down),
                null,
                null
            }
        ),
        UndeadAsylum4 = new ( 104,
            new[] {
                new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
                new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            }, new List<InteractiveObject> (new InteractiveObject[] {
                new Bonfire(12, 7),
                new Ladder(7, 8, true, new Destination(103, new Vector2(11, 2), Orientation.Down))
            }), new Destination[] {
                null,
                new (102, new Vector2(7, 1), Orientation.Down),
                null,
                null
            }
        );

    private static readonly Map[] MapArray = { UndeadAsylum1, UndeadAsylum2, UndeadAsylum3, UndeadAsylum4 };

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