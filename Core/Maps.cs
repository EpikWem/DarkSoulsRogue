namespace DarkSoulsRogue.Core;

public abstract class Maps
{

    public static readonly Map UndeadAsylum1 = new Map(
new int[][] {
        new int[] {1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1},
        new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
        new int[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1}
        
    });

    public class Map
    {
        public readonly int[][] WallsIds;
        public readonly int Width, Height;

        public Map(int[][] wallsIds)
        {
            WallsIds = wallsIds;
            Width = WallsIds[0].Length;
            Height = WallsIds.Length;
        }
        
    }
    
    
}