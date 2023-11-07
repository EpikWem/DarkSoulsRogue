using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public class Mob : Entity
{
    
    protected Mob(Armor baseArmor, Hitbox hitbox) : base(baseArmor, hitbox)
    {
        
    }
    
    //TODO: equipment and aggressive actions 
    
}