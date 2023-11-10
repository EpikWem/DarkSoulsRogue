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
    private static bool _isLevelUpping;

    public Bonfire(int xInGrid, int yInGrid) : base(xInGrid, yInGrid, Textures.BonfireT)
    {
        _isLevelUpping = false;
    }

    public override void Interact()
    {
        _isLevelUpping = false;
        
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

    public static bool IsLevelUpping()
        => _isLevelUpping;
    
    
    //===============================================
    // LEVELUP MENU
    //===============================================

    internal static class LevelUpMenu
    {
        
        private static readonly Rectangle LevelUpSheetArea = new(620, 20, 320, Camera.Height - 40);
        private const int SHeight = 32;
        private static int Sx()
            => LevelUpSheetArea.X + 2;

        private static int Sy(int row)
            => LevelUpSheetArea.Y + (row+3) * SHeight + 3;

        private static int[] _newValues;
        private static int _selectionId;
        
        internal static void Reset()
        {
            _isLevelUpping = true;
            _newValues = GameScreen.Character.Attributes.GetValues();
            _selectionId = 1;
        }

        internal static void Update()
        {
            if (Control.Interact.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuConfirm);
                
            }
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuBack);
                _isLevelUpping = false;
            }
            if (Control.MenuUp.IsOnePressed() && _selectionId > 1)
            {
                Sounds.Play(Sounds.SMenuMove);
                _selectionId--;
            }
            if (Control.MenuDown.IsOnePressed() && _selectionId < _newValues.Length-2)
            {
                Sounds.Play(Sounds.SMenuMove);
                _selectionId++;
            }
        }

        internal static void Draw()
        {
            var pos = new Vector2(LevelUpSheetArea.X, LevelUpSheetArea.Y);
            Main.SpriteBatch.Draw(Main.PixelTexture, LevelUpSheetArea, Colors.Black);
            Main.SpriteBatch.Draw(Main.PixelTexture, new Rectangle(Sx(), Sy(_selectionId), LevelUpSheetArea.Width-8, SHeight), Colors.DarkGray);
            Main.SpriteBatch.DrawString(Fonts.FontBold18, GameScreen.Character.Name, pos + new Vector2(10, 10), Colors.White);
            for (var i = 0; i < Attributes.NumAttributes; i++)
            {
                var dY = i == 0 ? pos.Y + 60 : pos.Y + 80 + i * 32;
                Main.SpriteBatch.Draw(Textures.IconsAttributes[i], pos + new Vector2(10, dY), Colors.White);
                Main.SpriteBatch.DrawString(Fonts.Font16, Attributes.GetName(i), pos + new Vector2(45, dY+4), Colors.LightGray);
                Main.SpriteBatch.DrawString(Fonts.FontBold18, _newValues[i].ToString(), pos + new Vector2(280, dY+1), Colors.White);
            }
            
        }
        
        
    }
    
}