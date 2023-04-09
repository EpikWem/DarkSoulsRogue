using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public class Mob : Entity
{
    
    protected Mob(Texture2D[] textures, Hitbox hitbox) : base(textures, hitbox)
    {
        
    }
    
    //TODO: equipment and aggressive actions 
    
}