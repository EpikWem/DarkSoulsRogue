namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{
    public const string Name = "bonfire";
    public const int StateNumber = 5; 
    
    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid)
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
        if (character.IsHuman)
        {
            if (character.Attributes.Get(Attributes.Attribute.Humanity) > 0)
            {
                if (State == 2 && !character.HasRiteOfKindling)
                    return;
                if (State != 4)
                {
                    character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
                    IncreaseState();
                }
            }
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