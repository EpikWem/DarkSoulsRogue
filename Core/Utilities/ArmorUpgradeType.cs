using DarkSoulsRogue.Core.Items;

namespace DarkSoulsRogue.Core.Utilities;

public class ArmorUpgrade
{

    public static readonly ArmorUpgrade
        Light = new(Mineral.TitaniteShard, new []{1, 1, 2, 3}),
        Heavy = new(Mineral.TitaniteShard, new []{1, 2, 2, 4}),
        Unique = new(Mineral.TwinklingTitanite, new []{1, 1, 2, 4});

    public readonly Mineral UpgradeMineral;
    public readonly int[] Quantities;

    private ArmorUpgrade(Mineral upgradeMineral, int[] quantities)
    {
        UpgradeMineral = upgradeMineral;
        Quantities = quantities;
    }

}