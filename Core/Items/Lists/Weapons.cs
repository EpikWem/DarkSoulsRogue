using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items.Lists;

public static class Weapons
{

    public static Weapon
        Claymore = new ("Claymore", Weapon.WeaponTypes.GreatSword),
        AstoraSword = new ("Sword of Astora", Weapon.WeaponTypes.StraightSword, Upgrades.Unique);

}