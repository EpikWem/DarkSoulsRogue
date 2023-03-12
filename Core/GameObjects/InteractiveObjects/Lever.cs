namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Lever : InteractiveObject
{
    
    public const string Name = "lever";
    public const int StateNumber = 1;

    private bool _isLocked;
    
    public Lever(int xInGrid, int yInGrid, bool isLocked = false) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        _isLocked = isLocked;
    }

    public override void Interact(Character character)
    {
        
    }

    public bool IsLocked() => _isLocked;

}