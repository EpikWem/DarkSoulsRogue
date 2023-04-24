using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public class SettingsMenu : Menu
{

    private readonly GeneralMenu _generalM;
    private readonly ControlsMenu _controlsM;
    private Menu _currentMenu;
    
    public SettingsMenu()
    {
        _generalM = new GeneralMenu();
        _controlsM = new ControlsMenu();
        _currentMenu = _generalM;
    }

    private void SwitchMenu()
    {
        _currentMenu = _currentMenu == _generalM ? _controlsM : _generalM;
        _currentMenu.Reinit();
    }
    
    internal override void Reinit()
    {
        _generalM.Reinit();
        _controlsM.Reinit();
        _currentMenu = _generalM;
    }

    internal override void Update()
    {
        if (Control.Consumable.IsOnePressed() || Control.Catalyst.IsOnePressed() || Control.Weapon.IsOnePressed() || Control.Shield.IsOnePressed())
        {
            SwitchMenu();
            return;
        }
        _currentMenu.Update();
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, IngameMenu.Area, Color.Black);
        _currentMenu.Draw(spriteBatch);
    }
    
}

internal class GeneralMenu : Menu 
{
    
    internal override void Reinit()
    {
        
    }

    internal override void Update()
    {
        
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "General Settings", new Vector2(Camera.Width/2 - 260, 300), Color.White);
    }
    
}

internal class ControlsMenu : Menu
{

    private static readonly Control[] Controls = {
        Control.Up, Control.Down, Control.Right, Control.Left, Control.Interact, Control.Run,
        Control.Weapon, Control.Shield, Control.Consumable, Control.Catalyst, Control.SwitchConsumable, Control.SwitchSpell,
        Control.Write, Control.MenuUp, Control.MenuDown, Control.MenuRight, Control.MenuLeft, Control.MenuBack };
    private static readonly string[] Names = {
        "Up", "Down", "Right", "Left", "Interact", "Run",
        "Weapon", "Shield", "Consumable", "Catalyst", "Switch Consum.", "Switch Spell",
        "Write/Mark", "(Menu) Up", "(Menu) Down", "(Menu) Right", "(Menu) Left", "(Menu) Back" };
    private const int X1 = 60, X2 =  X1 + Camera.Width / 3, X3 =  X2 + Camera.Width / 3, Y = 240,
        ItemHeight = 48, SecondColumn = 6, ThirdColumn = 12;

    private bool _waitingForKey;
    private RectangleHollow _selection;
    private int _selectionId;

    private void ChangeControl()
    {
        _waitingForKey = true;
        //TODO:
    }

    internal ControlsMenu() => Reinit();
    
    internal sealed override void Reinit()
    {
        _waitingForKey = false;
        _selection = new RectangleHollow(0, 0, 130, 30, Color.Orange, Color.Black, 2);
        _selectionId = 0;
    }

    internal override void Update()
    {
        if (_waitingForKey)
        {
            //TODO
            return;
        }
            
        
        if (Control.Interact.IsOnePressed())
        {
            ChangeControl();
            return;
        }
        
        if (Control.MenuUp.IsOnePressed() && _selectionId > FirstChoice())
            _selectionId--;
        if (Control.MenuDown.IsOnePressed() && _selectionId < LastChoice())
            _selectionId++;
        if (Control.MenuRight.IsOnePressed() && _selectionId < ThirdColumn)
            _selectionId += SecondColumn;
        if (Control.MenuLeft.IsOnePressed() && _selectionId >= SecondColumn)
            _selectionId -= SecondColumn;

        _selection.SetPosition(GetPosition(_selectionId) + new Vector2(-8, -8));
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "Controls Settings", new Vector2(Camera.Width/2 - 260, 20), Color.White);
        _selection.Draw(spriteBatch);
        for (var i = 0; i < Names.Length; i++)
            spriteBatch.DrawString(Fonts.Font, Names[i], GetPosition(i), Color.White);
    }
    
    private static Vector2 GetPosition(int index)
        => index < SecondColumn ? new Vector2(X1, index*ItemHeight + Y) :
            index < ThirdColumn ? new Vector2(X2, (index - SecondColumn)*ItemHeight + Y)
                                : new Vector2(X3, (index - ThirdColumn)*ItemHeight + Y);

    private static int FirstChoice() => 0;
    private static int LastChoice() => Names.Length - 1;
    
}