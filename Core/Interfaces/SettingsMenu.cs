﻿using System.Linq;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    public bool IsWaitingForKey() => _controlsM._waitingForKey;

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

    private const int CRWidth = 480, CRHeight = 60;
    private static readonly RectangleBordered CenteredRectangle
        = new((Camera.Width - CRWidth)/2, (Camera.Height - CRHeight)/2, CRWidth, CRHeight, Color.Gray, Color.Black, 2);

    internal bool _waitingForKey;
    private RectangleBordered _selection;
    private int _selectionId;

    internal ControlsMenu() => Reinit();
    
    internal sealed override void Reinit()
    {
        _waitingForKey = false;
        _selection = new RectangleBordered(0, 0, 130, 30, Color.Orange, Color.Black, 2);
        _selectionId = 0;
    }

    internal override void Update()
    {
        if (_waitingForKey) // get a key for replace
        {
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                _waitingForKey = false;
                return;
            }

            var key = Keys.None;
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                key = Keyboard.GetState().GetPressedKeys()[0]; // get it
            if (key == Keys.None || Controls.Any(c => key == (Keys)c.KeyCode()))
                return; // refuse if this key is already assigned
            Controls[_selectionId].Set(key); // save to current memory
            SettingsSystem.Save(); // save to settings file
            _waitingForKey = false; // unfreeze menu
            return;
        }
        
        if (Control.Interact.IsOnePressed()) // enter in changing mode
        {
            _waitingForKey = true;
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
        for (var i = 0; i < Names.Length; i++){
            spriteBatch.DrawString(Fonts.Font, Names[i], GetPosition(i), Color.White);
            //TODO: display current assigned keys
        }
        if (!_waitingForKey)
            return;
        CenteredRectangle.Draw(spriteBatch);
        spriteBatch.DrawString(Fonts.FontSoulCounter, "Waiting for an unassigned Key...", CenteredRectangle.GetPosition()*1.06f, Color.White);
    }
    
    private static Vector2 GetPosition(int index)
        => index < SecondColumn ? new Vector2(X1, index*ItemHeight + Y) :
            index < ThirdColumn ? new Vector2(X2, (index - SecondColumn)*ItemHeight + Y)
                                : new Vector2(X3, (index - ThirdColumn)*ItemHeight + Y);

    private static int FirstChoice() => 0;
    private static int LastChoice() => Names.Length - 1;

}