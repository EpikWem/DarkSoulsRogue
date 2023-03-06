using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items;

public class Equipment : Item
{
    
    private Upgrades.Upgrade _upgrade;
    private int _upgradeLevel;
    
    public Equipment(string name, Categories category) : base(name, category)
    {
        _upgrade = Upgrades.Regular;
    }
    
    public Equipment(string name, Categories category, Upgrades.Upgrade upgrade) : base(name, category)
    {
        _upgrade = upgrade;
    }

    public Mineral GetUpgradeMineral() => _upgrade.UpgradeMineral;

}