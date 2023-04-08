namespace DarkSoulsRogue.Core.Items;

public class WeaponUpgrade
{
    
    public static readonly WeaponUpgrade
        Regular = new("", Mineral.TitaniteShard, new []{1, 1, 2, 2, 4}),
        RegularMiddle = new("", Mineral.TitaniteShardLarge, new []{1, 1, 2, 2, 4}, Regular),
        RegularGreat = new("", Mineral.TitaniteChunk, new []{1, 1, 2, 3}, RegularMiddle),
        RegularPerfect = new("", Mineral.TitaniteSlab, new int[]{1}, RegularGreat),
        Raw = new("Raw", Mineral.TitaniteShardLarge, new []{1, 1, 2, 2, 4}, Regular),
        Lightning = new("Lightning", Mineral.TitaniteChunk, new []{1, 1, 2, 2, 4}, RegularMiddle),
        Crystal = new("Crystal", Mineral.TitaniteChunk, new []{1, 1, 2, 2, 4}, RegularMiddle),
        Magic = new("Magic", Mineral.GreenTitanite, new []{1, 1, 2, 2, 4}, Regular),
        MagicGreat = new("Magic", Mineral.BlueTitanite, new []{1, 1, 2, 2, 3}, Magic),
        MagicPerfect = new("Magic", Mineral.BlueTitaniteSlab, new []{1}, MagicGreat),
        Enchanted = new("Enchanted", Mineral.BlueTitanite, new []{1, 1, 2, 2, 3}, Magic),
        EnchantedPerfect = new("Enchanted", Mineral.BlueTitaniteSlab, new []{1}, Enchanted),
        Fire = new("Fire", Mineral.GreenTitanite, new []{1, 1, 2, 2, 4}, Regular),
        FireGreat = new("Fire", Mineral.RedTitanite, new []{1, 1, 2, 2, 4}, Fire),
        FirePerfect = new("Fire", Mineral.RedTitaniteSlab, new []{1}, FireGreat),
        Chaos = new("Chaos", Mineral.RedTitanite, new []{1, 1, 2, 2, 4}, Fire),
        ChaosPerfect = new("Chaos", Mineral.RedTitaniteSlab, new []{1}, Chaos),
        Divine = new("Divine", Mineral.GreenTitanite, new []{1, 1, 2, 2, 4}, Regular),
        DivineGreat = new("Divine", Mineral.WhiteTitanite, new []{1, 1, 2, 2, 4}, Divine),
        DivinePerfect = new("Divine", Mineral.WhiteTitaniteSlab, new []{1}, DivineGreat),
        Occult = new("Occult", Mineral.WhiteTitanite, new []{1, 1, 2, 2, 4}, Divine),
        OccultPerfect = new("Occult", Mineral.WhiteTitaniteSlab, new []{1}, Occult),
        Unique = new("", Mineral.TwinklingTitanite, new []{1, 1, 2, 2, 4}),
        Dragon = new("", Mineral.DragonScale, new []{1, 1, 2, 2, 4}),
        Boss = new("", Mineral.DemonTitanite, new []{1, 1, 2, 2, 4}, RegularMiddle);

    public readonly string Name;
    public readonly Mineral UpgradeMineral;
    public readonly int[] Quantities;
    public readonly WeaponUpgrade BaseWeaponUpgrade;

    public WeaponUpgrade(string name, Mineral upgradeMineral, int[] quantities, WeaponUpgrade baseWeaponUpgrade = null)
    {
        Name = name;
        UpgradeMineral = upgradeMineral;
        Quantities = quantities;
        BaseWeaponUpgrade = baseWeaponUpgrade;
    }

    public int GetBaseUpgradeNumber()
        => BaseWeaponUpgrade == null
            ? 0
            : BaseWeaponUpgrade.Quantities.Length + BaseWeaponUpgrade.GetBaseUpgradeNumber();

}