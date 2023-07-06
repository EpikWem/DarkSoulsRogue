using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class IngameMenu
{

    private const int X = 150, Y = 100;
    public static readonly Rectangle Area = new(X, Y, Camera.Width - X*2, Camera.Height - Y*2);

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

    internal static void Deactivate() => _isActive = false;
    
    private static void SwitchMenu()
    {
        _currentMenu = _currentMenu == EquipmentM ? ItemsM : EquipmentM;
        _currentMenu.Reinit();
    }

    internal static void ToggleQuit()
    {
        _quitting = !_quitting;
        if (_quitting)
            QuitM.Reinit();
    }
    
    public static void Update()
    {
        if (Control.Pause.IsOnePressed())
        {
            _isActive = false;
            return;
        }
        if (_quitting)
        {
            QuitM.Update();
            return;
        }
        if (Control.MenuBack.IsOnePressed())
            ToggleQuit();
        if (Control.Consumable.IsOnePressed() || Control.Catalyst.IsOnePressed() || Control.Weapon.IsOnePressed() || Control.Shield.IsOnePressed())
            SwitchMenu();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, Area, Color.Black);
        _currentMenu.Draw(spriteBatch);
        if (_quitting)
            QuitM.Draw(spriteBatch);
    }

}

internal class EquipmentMenu : Menu
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
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "EQUIPMENTS", new Vector2(IngameMenu.Area.X + 40, IngameMenu.Area.Y + 40), Color.White);
    }
    
}



internal class ItemsMenu : Menu
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
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "I T E M S", new Vector2(IngameMenu.Area.X + 40, IngameMenu.Area.Y + 40), Color.White);
    }
    
}

internal class QuitMenu : Menu
{
    
    private const int QWidth = 500, QHeight = 200;
    private static readonly RectangleBordered QArea = new((Camera.Width - QWidth)/2, (Camera.Height - QHeight)/2, QWidth, QHeight, Color.WhiteSmoke, Color.Black, 4);
    private bool _selection;

    internal QuitMenu() => Reinit();

    internal sealed override void Reinit()
    {
        _selection = false;
    }
    
    internal override void Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            if (_selection) // if OK
            {
                IngameMenu.Deactivate();
                Main.GotoTitle();
                return;
            }
            // if GO BACK
            IngameMenu.ToggleQuit();
            return;
        }
        if (Control.MenuBack.IsOnePressed())
        { // GO BACK
            IngameMenu.ToggleQuit();
            return;
        }
        if (Control.MenuRight.IsOnePressed() || Control.MenuLeft.IsOnePressed())
            _selection = !_selection;
    }
    
    internal override void Draw(SpriteBatch spriteBatch)
    {
        QArea.Draw(spriteBatch);
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(QArea.Rectangle.X + 30 + (_selection ? 90 : 0), QArea.Rectangle.Y + 62, 70, 30), Colors.Orange);
        spriteBatch.DrawString(Fonts.FontSoulCounter, "Quit to Title ?", new Vector2(QArea.Rectangle.X+10, QArea.Rectangle.Y+10), Colors.White);
        spriteBatch.DrawString(Fonts.FontSoulCounter, "Back     Yes", new Vector2(QArea.Rectangle.X+30, QArea.Rectangle.Y+64), Colors.White);
    }
    
}