using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{
    public const string Name = "bonfire";
    public const int StateNumber = 5; 
    
    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        
    }

    public override void Interact(Character character)
    {
        character.AddLife(character.MaxLife());
        character.AddStamina(character.MaxStamina());
        Lit(character);
    }

    private void Lit(Character character)
    {
        if (State != 0)
        {
            if (character.IsHuman)
            {
                if (character.Attributes.Get(Attributes.Attribute.Humanity) > 0)
                {
                    if (State == 1 || ((State == 2 || State == 3) && character.Triggers.Get(Triggers.Trigger.HasRiteOfKindling)))
                    {
                        character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
                        IncreaseState();
                    }
                    else
                    {
                        return; // impossible d'embraser plus
                    }
                    
                }
                else
                {
                    return; // pas assez d'humanité
                }
            }
            else
            {
                RetrieveHumanity(character);
                return; // impossible d'embraser sous forme de carcasse
            }
        }
        else
        {
            IncreaseState(); // Fire Lit!
        }
    }

    private void RetrieveHumanity(Character character)
    {
        if (!character.IsHuman)
        {
            if (character.Attributes.Get(Attributes.Attribute.Humanity) > 0)
            {
                character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
                character.IsHuman = true;
            }   
        }
    }
    
}