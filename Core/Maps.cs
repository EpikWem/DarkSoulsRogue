using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public abstract class Maps
{

    public static Map UndeadAsylum1 = new Map(
new int[][] {
        new int[] {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[] {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1}
        
    });

    public class Map
    {
        public readonly int[] WallsIds;

        public Map(int[][] wallsIds)
        {
            for (int y = 0; y < GameMain.GridHeight; y++)
            {
                for (int x = 0; x < GameMain.GridWidth; x++)
                {
                    WallsIds[y*GameMain.GridWidth + x] = wallsIds[y][x];
                }
            }
        }
        
    }
    
    
}