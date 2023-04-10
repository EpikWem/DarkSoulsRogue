using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Statics;

public class Map
{

    private static readonly Map
        UndeadAsylum1 = new (101, new[] {
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
            new List<InteractiveObject> {
                new LockedDoor(7, 2, Key.AsylumCell),
                new Loot(9, 6, new Stack(Key.AsylumCell)) },
            new List<Entity>(),
            new Destination[] {
                new (102, new Vector2(7, 8), Orientation.Up),
                null,
                null,
                null }
        ),
        UndeadAsylum2 = new (102, new[] {
                new[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 } },
            new List<InteractiveObject> (),
            new List<Entity>(),
            new Destination[] {
                new (103, new Vector2(4, 9), Orientation.Up),
                new (101, new Vector2(7, 3), Orientation.Down), 
                null,
                null }
        ),
        UndeadAsylum3 = new ( 103, new[] {
                new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0},
                new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0},
                new[] {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                new[] {0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0} },
            new List<InteractiveObject> {
                new Ladder(11, 1, false, new Destination(104, new Vector2(7, 7), Orientation.Up)) },
            new List<Entity>(),
            new Destination[] {
                null,
                new (102, new Vector2(7, 1), Orientation.Down),
                null,
                null }
        ),
        UndeadAsylum4 = new ( 104, new[] {
                new[] {1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
                new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} },
            new List<InteractiveObject> {
                new Door(7, 0),
                new OnewayDoor(14, 3, Orientation.Left),
                new Bonfire(7, 4),
                new Ladder(7, 8, true, new Destination(103, new Vector2(11, 2), Orientation.Down)) },
            new List<Entity>(),
            new Destination[] {
                new (105, new Vector2(7, 9), Orientation.Up),
                null,
                new (107, new Vector2(7, 1), Orientation.Down),
                null }
        ),
        UndeadAsylum5 = new ( 105, new[] {
                new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new[] {1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1} },
            new List<InteractiveObject> (), 
            new List<Entity> {
                new Npc(new Vector2(7, 2), Textures.ArmorTextures[2], new Hitbox(4, 2), Orientation.Down, Speaks.Solarius) },
            new Destination[] {
                null,
                new (104, new Vector2(7, 1), Orientation.Down),
                null,
                new (106, new Vector2(14, 3), Orientation.Left) }
        );

    private static readonly Map[] MapArray = { UndeadAsylum1, UndeadAsylum2, UndeadAsylum3, UndeadAsylum4, UndeadAsylum5 };

    public static int GetObjectsCount() => MapArray.Sum(map => map.Objects.Count);
    
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

    public static InteractiveObject GetObject(int id)
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
    
    public readonly int Id;
    public readonly int[][] WallsIds;
    public readonly int Width, Height;
    public readonly List<InteractiveObject> Objects;
    public readonly List<Entity> Entities;
    public readonly Destination[] Connections;

    public Map(int id, int[][] walls, List<InteractiveObject> objects, List<Entity> entities, Destination[] connections)
    {
        Id = id;
        WallsIds = walls;
        Width = WallsIds[0].Length;
        Height = WallsIds.Length;
        Objects = objects;
        Entities = entities;
        Connections = connections;
    }

}