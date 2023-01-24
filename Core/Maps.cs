using SharpDX.MediaFoundation;

namespace DarkSoulsRogue.Core;

public abstract class Maps
{

    public Map UndeadAsylum1 = new Map({});

    public class Map
    {
        private readonly Wall[] _walls;

        public Map(int[] walls)
        {
            _walls = new Wall[walls.Length];
            for (int i = 0; i < _walls.Length; i++)
            {
                _walls[i] = walls[i];
            }
        }

        public Wall GetWall(int i)
        {
            return _walls[i];
        }
        
        public Wall GetWall(int gx, int gy)
        {
            return _walls[gy * GameMain.GridWidth + gx];
        }
        
        public int WallNumber()
        {
            return _walls.Length;
        }
        
    }
    
    
}