namespace DarkSoulsRogue.Core.Items.Lists;

public static class Upgrades
{

    public static readonly Upgrade
        Regular = new (Minerals.TitaniteShard, null),
        RegularMiddle = new (Minerals.TitaniteShardLarge, Regular),
        RegularGreat = new (Minerals.TitaniteChunk, RegularMiddle),
        Raw = new (Minerals.TitaniteShardLarge, Regular),
        Lightning = new (Minerals.TitaniteChunk, RegularMiddle),
        Crystal = new (Minerals.TitaniteChunk, RegularMiddle),
        Magic = new (Minerals.GreenTitanite, Regular),
        MagicGreat = new (Minerals.BlueTitanite, Magic),
        Enchanted = new (Minerals.BlueTitanite, Magic),
        Fire = new (Minerals.GreenTitanite, Regular),
        FireGreat = new (Minerals.RedTitanite, Fire),
        Chaos = new (Minerals.RedTitanite, Fire),
        Divine = new (Minerals.GreenTitanite, Regular),
        DivineGreat = new (Minerals.WhiteTitanite, Divine),
        Occult = new (Minerals.WhiteTitanite, Divine),
        Unique = new (Minerals.TwinklingTitanite, null),
        Dragon = new (Minerals.DragonScale, null),
        Boss = new (Minerals.DemonTitanite, RegularMiddle);

    public class Upgrade
    {
        public readonly Mineral UpgradeMineral;
        public readonly Upgrade BaseUpgrade;

        public Upgrade(Mineral upgradeMineral)
        {
            UpgradeMineral = upgradeMineral;
            BaseUpgrade = null;
        }
        
        public Upgrade(Mineral upgradeMineral, Upgrade baseUpgrade)
        {
            UpgradeMineral = upgradeMineral;
            BaseUpgrade = baseUpgrade;
        }
    } 
    
}