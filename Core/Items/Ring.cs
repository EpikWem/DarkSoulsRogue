using System;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Items;

public abstract class Ring : Item
{

    private readonly Func<Character, int> _effect;
    
    public Ring(string name, Func<Character, int> effect) : base(name, Categories.Ring)
    {
        
    }

    public void Effect(Character character) => _effect(character);

}