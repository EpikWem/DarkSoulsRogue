using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Lever : InteractiveObject
{
    
    public const string Name = "lever";
    public const int StateNumber = 2;

    private bool _isLocked;

    public Lever(int xInGrid, int yInGrid) : base(xInGrid, yInGrid)
    {
        _isLocked = false;
    }
    
    public Lever(int xInGrid, int yInGrid, bool isLocked) : base(xInGrid, yInGrid)
    {
        _isLocked = isLocked;
    }

    public override void Interact(Character character)
    {
        
    }

    public bool IsLocked() => _isLocked;

}