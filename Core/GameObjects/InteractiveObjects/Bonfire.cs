using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.GameObjects.InteractiveObjects;

public class Bonfire : InteractiveObject
{
    public const string Name = "bonfire";
    public const int StateNumber = 5;
    
    public static readonly SelectionBar Menu = new("Bonfire", new []{"Level Up", "Warp", "Reverse Hollowing", "Kindle", "Bottomless Box", "Attune Magic", "Smith"});
    private static Bonfire _activeBonfire;

    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        // Nothing to init...
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

    public static void Update()
    {
        if (!Menu.Update())
            return;
        
        switch (Menu.SelectionId())
        {
            case 0: LevelUpMenu.Reset(); break;
            case 1:  break;
            case 2: RetrieveHumanity(); break;
            case 3: _activeBonfire.Kindle(); break;
            case 4:  break;
            case 5:  break;
            case 6:  break;
            default: return;
        }
    }

    private void Kindle()
    {
        if (!GameScreen.Character.IsHuman)
        {
            Notification.Reset("Can't kindle while Hollow");
            return;
        }
        if (State == 4 || (State == 2 && !GameScreen.Character.Triggers.Get(Triggers.Trigger.HasRiteOfKindling)))
        {
            Notification.Reset("Can't kindle more");
            return;
        }
        if (GameScreen.Character.Attributes.Get(Attributes.Attribute.Humanity) < 1)
        {
            Notification.Reset("Humanity needed");
            return;
        }
        
        Sounds.Play(Sounds.SPyromancy);
        GameScreen.Character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
        IncreaseState();
    }

    private static void RetrieveHumanity()
    {
        if (GameScreen.Character.IsHuman)
        {
            Notification.Reset("Already Human");
            return;
        }
        if (GameScreen.Character.Attributes.Get(Attributes.Attribute.Humanity) < 1)
        {
            Notification.Reset("Humanity needed");
            return;
        }
        BigMessage.Reset("Humanity Retrieved", 180, Colors.Gold);
        GameScreen.Character.Attributes.Increase(Attributes.Attribute.Humanity, -1);
        GameScreen.Character.IsHuman = true;
    }

    

}