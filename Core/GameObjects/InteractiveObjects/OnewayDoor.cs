using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class OnewayDoor : Door
{

    private readonly Orientation _rightOrientation;
    
    public OnewayDoor(int xInGrid, int yInGrid, Orientation rightOrientation) : base(xInGrid, yInGrid)
    {
        _rightOrientation = rightOrientation;
    }

    public override void Interact()
    {
        if (State != 0)
            return;
        if (GameScreen.Character.Orientation == _rightOrientation)
        {
            Sounds.Play(Sounds.SChest);
            IncreaseState();
        }
        else
            Notification.Reset("It can't opened on this side.");
    }

}