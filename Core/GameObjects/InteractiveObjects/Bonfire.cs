using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{
    public const string Name = "bonfire";
    public const int StateNumber = 5;
    
    public static readonly SelectionBar Menu = new("Bonfire", new []{"Level Up", "Warp", "Reverse Hollowing", "Kindle", "Bottomless Box", "Attune Magic", "Smith"});
    
    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        
    }

    public override void Interact()
    {
        if (State == 0)
        {
            //Sounds.Play(Sounds.SBonfireLit);
            IncreaseState();
            // SAY: Fire Lit!
            return;
        }
        
        Menu.Reset();
        GameScreen.Character.AddLife(GameScreen.Character.MaxLife());
        GameScreen.Character.AddStamina(GameScreen.Character.MaxStamina());
    }

    private void Kindle(Character character)
    {
        if (!character.IsHuman)
        {
            //SAY: impossible d'embraser sous forme de carcasse
            return;
        }
        if (State == 4 || (State == 2 && !character.Triggers.Get(Triggers.Trigger.HasRiteOfKindling)))
        { 
            //SAY: impossible d'embraser plus
            return;
        }

        if (character.Attributes.Get(Attributes.Attribute.Humanity) < 1)
        { 
            //SAY: pas assez d'humanité
            return;
        }
        
        //PLAYSOUND: Kindle
        character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
        IncreaseState();
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