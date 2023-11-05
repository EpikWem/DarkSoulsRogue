using System.Windows.Forms.VisualStyles;
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
    private static bool _inItemsMenu;
    private static bool _quitting;

    public static bool IsActive() => _isActive;
    public static void Activate()
    {
        _isActive = true;
        _inItemsMenu = false;
        _quitting = false;
    }

    internal static void Deactivate() => _isActive = false;
    
    private static void SwitchMenu()
    {
        Sounds.Play(Sounds.SMenuMove);
        _inItemsMenu = !_inItemsMenu;
        if (_inItemsMenu)
            ItemsMenu.Reinit();
        else
            EquipmentMenu.Reinit();
    }

    internal static void ToggleQuit()
    {
        if (_quitting)
        {
            Sounds.Play(Sounds.SMenuBack);
        }
        else
        {
            Sounds.Play(Sounds.SMenuConfirm);
            QuitMenu.Reinit();
        }
        _quitting = !_quitting;
    }
    
    public static void Update()
    {
        if (Control.Pause.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuBack);
            _isActive = false;
            return;
        }
        if (_quitting)
        {
            QuitMenu.Update();
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
        if (_inItemsMenu)
            ItemsMenu.Draw(spriteBatch);
        else
            EquipmentMenu.Draw(spriteBatch);
        if (_quitting)
            QuitMenu.Draw(spriteBatch);
    }

}

internal static class EquipmentMenu
{
    
    static EquipmentMenu() => Reinit();
    internal static void Reinit()
    {
        
    }
    
    internal static void Update()
    {
        
    }
    
    internal static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "EQUIPMENTS", new Vector2(IngameMenu.Area.X + 40, IngameMenu.Area.Y + 40), Color.White);
    }
    
}



internal static class ItemsMenu
{
    
    static ItemsMenu() => Reinit();
    internal static void Reinit()
    {
        
    }
    
    internal static void Update()
    {
        
    }
    
    internal static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "I T E M S", new Vector2(IngameMenu.Area.X + 40, IngameMenu.Area.Y + 40), Color.White);
    }
    
}

internal static class QuitMenu
{
    
    private const int QWidth = 500, QHeight = 200;
    private static readonly RectangleBordered QArea = new((Camera.Width - QWidth)/2, (Camera.Height - QHeight)/2, QWidth, QHeight, Color.WhiteSmoke, Color.Black, 4);
    private static bool _selection;

    static QuitMenu() => Reinit();

    internal static void Reinit()
    {
        _selection = false;
    }
    
    internal static void Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            if (_selection) // if OK
            {
                Sounds.Play(Sounds.SMenuConfirm);
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
        {
            Sounds.Play(Sounds.SMenuMove);
            _selection = !_selection;
        }
    }
    
    internal static void Draw(SpriteBatch spriteBatch)
    {
        QArea.Draw(spriteBatch);
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(QArea.Rectangle.X + 30 + (_selection ? 90 : 0), QArea.Rectangle.Y + 62, 70, 30), Colors.Orange);
        spriteBatch.DrawString(Fonts.FontSoulCounter, "Quit to Title ?", new Vector2(QArea.Rectangle.X+10, QArea.Rectangle.Y+10), Colors.White);
        spriteBatch.DrawString(Fonts.FontSoulCounter, "Back     Yes", new Vector2(QArea.Rectangle.X+30, QArea.Rectangle.Y+64), Colors.White);
    }
    
}