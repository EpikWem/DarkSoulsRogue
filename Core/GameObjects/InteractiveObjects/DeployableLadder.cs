using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

/**
 *  STATES: 
 *      0: Collapsed,
 *      1: Deployed
 */
public class DeployableLadder : Ladder
{
    
    internal new const string Name = "deployableladder";
    internal new const int StateNumber = 2; 
    
    public DeployableLadder(Texture2D[] textures, int xInGrid, int yInGrid, int destinationMap) : base(textures, xInGrid, yInGrid, destinationMap)
    {
        
    }

    public new void Interact(Character character)
    {
        if (State == 0)
            State = 1;
        else
        {
            base.Interact(character);
        }

    }
    
}