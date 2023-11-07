using System;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.Items;

public class Consumable : Item
{

    private static Character _character;
    public static void Init(Character character)
        => _character = character;

    public readonly Action _effect; 

    public static readonly Consumable
        Soul1 = new("Soul of a Lost Undead", ()=>_character.AddSouls(200)),
        Soul2 = new("Large Soul of a Lost Undead", ()=>_character.AddSouls(400)),
        Soul3 = new("Soul of a Nameless Soldier", ()=>_character.AddSouls(800)),
        Soul4 = new("Large Soul of a Nameless Soldier", ()=>_character.AddSouls(1000)),
        Soul5 = new("Soul of a Proud Knight", ()=>_character.AddSouls(2000)),
        Soul6 = new("Large Soul of a Proud Knight", ()=>_character.AddSouls(3000)),
        Soul7 = new("Soul of a Brave Warrior", ()=>_character.AddSouls(5000)),
        Soul8 = new("Large Soul of a Brave Warrior", ()=>_character.AddSouls(8000)),
        Soul9 = new("Soul of a Hero", ()=>_character.AddSouls(10000)),
        Soul10 = new("Soul of a Great Hero", ()=>_character.AddSouls(20000)),
        SoulGolem = new("Core of an Iron Golem", ()=>_character.AddSouls(12000)),
        SoulGuardian = new("Guardian Soul", ()=>_character.AddSouls(12000)),
        SoulArtorias = new("Soul of Artorias", ()=>_character.AddSouls(16000)),
        SoulGwyn = new("Soul of Gwyn, Lord of Cinder", ()=>_character.AddSouls(20000)),
        SoulGwyndolin = new("Soul of Gwyndolin", ()=>_character.AddSouls(16000)),
        SoulManus = new("Soul of Manus", ()=>_character.AddSouls(18000)),
        SoulOrnstein = new("Soul of Ornstein", ()=>_character.AddSouls(12000)),
        SoulPriscilla = new("Soul of Priscilla", ()=>_character.AddSouls(12000)),
        SoulQuelaag = new("Soul of Quelaag", ()=>_character.AddSouls(8000)),
        SoulSif = new("Soul of Sif", ()=>_character.AddSouls(16000)),
        SoulSmough = new("Soul of Smough", ()=>_character.AddSouls(12000)),
        SoulButterfly = new("Soul of the Moonlight Butterfly", ()=>_character.AddSouls(1200)),
        Humanity = new("Humanity", ()=>_character.Attributes.Increase(Attributes.Attribute.Humanity, 1)),
        TwinHumanities = new("Twin Humanites", ()=>_character.Attributes.Increase(Attributes.Attribute.Humanity, 2));
    
    public Consumable(string name, Action effect) : base(name, Categories.Consumable)
    {
        _effect = effect;
    }

}