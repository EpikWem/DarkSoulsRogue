using DarkSoulsRogue.Core.Items;

namespace DarkSoulsRogue.Core.Utilities;

public class ParamBonus
{

    public static readonly float
        S = 1.5f,
        A = 1.0f,
        B = 0.8f,
        C = 0.6f,
        D = 0.4f,
        E = 0.2f,
        F = 0.0f;    
    public readonly float[] Bonuses;

    public ParamBonus(float strength, float dexterity, float intelligence, float faith)
    {
        Bonuses = new []{strength, dexterity, intelligence, faith};
    }

    public int GetBonusPhysicalDamages(Attributes attributes, Weapon weapon)
        => (int)(weapon.AtkPhysical * (
                Bonuses[0] * attributes.Get(Attributes.Attribute.Strength) +
                Bonuses[1] * attributes.Get(Attributes.Attribute.Dexterity)));

    public int GetBonusMagicDamages(Attributes attributes, Weapon weapon)
        => (int)(weapon.AtkMagic * (
            Bonuses[2] * attributes.Get(Attributes.Attribute.Intelligence) +
            Bonuses[3] * attributes.Get(Attributes.Attribute.Faith)));

    public int GetBonusChaosDamages(Attributes attributes, Weapon weapon)
        => (int)(weapon.AtkPhysical *
            0.1f * (attributes.Get(Attributes.Attribute.Humanity) > 10 ? 10 : attributes.Get(Attributes.Attribute.Humanity)));

}