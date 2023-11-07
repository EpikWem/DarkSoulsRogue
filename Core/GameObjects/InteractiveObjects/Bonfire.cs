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
    private static Bonfire _activeBonfire;

    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        // nothing to init
    }

    public override void Interact()
    {
        if (State == 0)
        {
            Sounds.Play(Sounds.SPyromancy);
            IncreaseState();
            BigMessage.Reset("Bonfire Lit", 180, Colors.Gold);
            return;
        }
        
        Sounds.Play(Sounds.SBonfireRest);
        _activeBonfire = this;
        Menu.Reset();
        GameScreen.Character.AddLife(GameScreen.Character.MaxLife());
        GameScreen.Character.AddStamina(GameScreen.Character.MaxStamina());
    }

    public static void Update(Character character)
    {
        if (Menu.Update())
        {
            switch (Menu.SelectionId())
            {
                case 0:  break;
                case 1:  break;
                case 2: RetrieveHumanity(character); break;
                case 3: _activeBonfire.Kindle(character); break;
                case 4:  break;
                case 5:  break;
                case 6:  break;
                default: return;
            }
        }
    }

    private void Kindle(Character character)
    {
        if (!character.IsHuman)
        {
            Notification.Reset("Can't kindle while Hollow");
            return;
        }
        if (State == 4 || (State == 2 && !character.Triggers.Get(Triggers.Trigger.HasRiteOfKindling)))
        {
            Notification.Reset("Can't kindle more");
            return;
        }
        if (character.Attributes.Get(Attributes.Attribute.Humanity) < 1)
        {
            Notification.Reset("Humanity needed");
            return;
        }
        
        Sounds.Play(Sounds.SPyromancy);
        character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
        IncreaseState();
    }

    private static void RetrieveHumanity(Character character)
    {
        if (character.IsHuman)
        {
            Notification.Reset("Already Human");
            return;
        }
        if (character.Attributes.Get(Attributes.Attribute.Humanity) < 1)
        {
            Notification.Reset("Humanity needed");
            return;
        }
        BigMessage.Reset("Humanity Retrieved", 180, Colors.Gold);
        character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
        character.IsHuman = true;
    }
    
}