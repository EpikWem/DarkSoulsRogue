using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class IngameMenu
{

    private const int X = 150, Y = 100;
    private static readonly Rectangle Area = new(X, Y, Camera.Width - X*2, Camera.Height - Y*2);

    private static bool _isActive;
    private static Menu _currentMenu;
    private static bool _quitting;
    private static readonly EquipmentMenu EquipmentM = new();
    private static readonly ItemsMenu ItemsM = new();
    private static readonly QuitMenu QuitM = new();

    public static bool IsActive() => _isActive;
    public static void Activate()
    {
        _isActive = true;
        _currentMenu = EquipmentM;
        _quitting = false;
    }
    
    private static void SwitchMenu()
    {
        if (_currentMenu == EquipmentM)
        {
            _currentMenu = ItemsM;
            _currentMenu.Reinit();
            return;
        }
        _currentMenu = EquipmentM;
        _currentMenu.Reinit();
    }

    private static void ToggleQuit()
    {
        _quitting = !_quitting;
        if (_quitting)
            QuitM.Reinit();
    }
    
    public static void Update()
    {
        if (Controls.Pause.IsOnePressed)
        {
            _isActive = false;
            return;
        }
        if (_quitting)
        {
            QuitM.Update();
            return;
        }
        if (Controls.MenuBack.IsOnePressed)
            ToggleQuit();
        if (Controls.Consumable.IsOnePressed || Controls.Catalyst.IsOnePressed || Controls.Weapon.IsOnePressed || Controls.Shield.IsOnePressed)
            SwitchMenu();
        
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, Area, Color.Black);
        _currentMenu.Draw(spriteBatch);
        if (_quitting)
            QuitM.Draw(spriteBatch);
    }
    
    

    private class EquipmentMenu : Menu
    {
        internal EquipmentMenu() => Reinit();
        internal sealed override void Reinit()
        {
            
        }
        internal override void Update()
        {
            
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.FontHumanityCounter, "EQUIPMENTS", new Vector2(Area.X + 40, Area.Y + 40), Color.White);
        }
    }

    
    
    private class ItemsMenu : Menu
    {
        internal ItemsMenu() => Reinit();
        internal sealed override void Reinit()
        {
            
        }
        internal override void Update()
        {
            
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.FontHumanityCounter, "I T E M S", new Vector2(Area.X + 40, Area.Y + 40), Color.White);
        }
    }

    
    
    private class QuitMenu : Menu
    {
        private const int QWidth = 500, QHeight = 200;
        private static readonly RectangleHollow QArea = new((Camera.Width - QWidth)/2, (Camera.Height - QHeight)/2, QWidth, QHeight, Color.WhiteSmoke, Color.Black, 4);
        private bool _selection;

        internal QuitMenu() => Reinit();

        internal sealed override void Reinit()
        {
            _selection = false;
        }
        internal override void Update()
        {
            if (Controls.Interact.IsOnePressed)
            {
                if (_selection) // if OK
                {
                    _isActive = false;
                    Main.GotoTitle();
                    return;
                }
                // if GO BACK
                ToggleQuit();
                return;
            }
            if (Controls.MenuBack.IsOnePressed)
            { // GO BACK
                ToggleQuit();
                return;
            }
            if (Controls.MenuRight.IsOnePressed || Controls.MenuLeft.IsOnePressed)
                _selection = !_selection;
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            QArea.Draw(spriteBatch);
            new RectangleHollow(QArea.Rectangle.X+30 + (_selection ? 90 : 0), QArea.Rectangle.Y+62, 70, 30, Color.Orange, Color.Black, 2).Draw(spriteBatch);
            spriteBatch.DrawString(Fonts.FontSoulCounter, "Quit to Title ?", new Vector2(QArea.Rectangle.X+10, QArea.Rectangle.Y+10), Color.White);
            spriteBatch.DrawString(Fonts.FontSoulCounter, "Back     Yes", new Vector2(QArea.Rectangle.X+30, QArea.Rectangle.Y+64), Color.White);
        }
    }

}