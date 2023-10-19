using System.Linq;
using DarkSoulsRogue.Core.Statics;
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
        if ((Control.Consumable.IsOnePressed() || Control.Catalyst.IsOnePressed() || Control.Weapon.IsOnePressed() || Control.Shield.IsOnePressed())
            && !_controlsM.WaitingForKey && !_controlsM.JustChangedAKey)
            SwitchMenu();
        _currentMenu.Update();
        if (!_controlsM.JustChangedAKey)
            _controlsM.JustChangedAKey = false;
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, IngameMenu.Area, Colors.Black);
        _currentMenu.Draw(spriteBatch);
    }

    public bool IsWaitingForKey() => _controlsM.WaitingForKey;

}

internal class GeneralMenu : Menu
{

    private int[] _levels;
    private int _selection;

    internal override void Reinit()
    {
        _levels = Sounds.GetLevels();
        _selection = 0;
    }

    internal override void Update()
    {
        if (Control.MenuUp.IsOnePressed())
            _selection = 0;
        if (Control.MenuDown.IsOnePressed())
            _selection = 1;
        if (Control.MenuRight.IsOnePressed())
        {
            _levels[_selection] += 10;
            if (_levels[_selection] > 100)
                _levels[_selection] = 100;
            ApplyChanges();
        }
        if (Control.MenuLeft.IsOnePressed())
        {
            _levels[_selection] -= 10;
            if (_levels[_selection] < 0)
                _levels[_selection] = 0;
            ApplyChanges();
        }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "General Settings", new Vector2(Camera.Width/2 - 260, 100), Colors.White);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Music).ToString(), new Vector2(300, 300), Colors.White);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Sfx).ToString(), new Vector2(300, 400), Colors.White);
    }

    private void ApplyChanges()
    {
        Sounds.SetLevels(_levels);
        SettingsSystem.Save();
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
        "Weapon", "Shield", "Consumable", "Catalyst", "Switch Cons.", "Switch Spell",
        "Write/Mark", "(Menu) Up", "(Menu) Down", "(Menu) Right", "(Menu) Left", "(Menu) Back" };
    private const int X1 = 60, X2 =  X1 + Camera.Width / 3, X3 =  X2 + Camera.Width / 3, Y = 240,
        ItemHeight = 48, SecondColumn = 6, ThirdColumn = 12;

    private const int CRWidth = 480, CRHeight = 60;
    private static readonly RectangleBordered CenteredRectangle
        = new((Camera.Width - CRWidth)/2, (Camera.Height - CRHeight)/2, CRWidth, CRHeight, Color.Gray, Colors.Black, 2);

    internal bool WaitingForKey;
    internal bool JustChangedAKey;
    private RectangleBordered _selection;
    private int _selectionId;

    internal ControlsMenu() => Reinit();
    
    internal sealed override void Reinit()
    {
        WaitingForKey = false;
        _selection = new RectangleBordered(0, 0, 144, 30, Colors.Orange, Colors.Orange, 2);
        _selectionId = 0;
    }

    internal override void Update()
    {
        if (WaitingForKey) // get a key for replace
        {
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                WaitingForKey = false;
                return;
            }

            var key = Keys.None;
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                key = Keyboard.GetState().GetPressedKeys()[0]; // get it
            if (key == Keys.None || Controls.Any(c => c.KeyCode() == (int)key) // already assigned
                                 || key == Keys.Escape || key == Keys.Enter || key == Keys.Back) // forbidden keys
                return; // refuse if this key is already assigned
            Controls[_selectionId].Set(key); // save to current memory
            SettingsSystem.Save(); // save to settings file
            WaitingForKey = false; // unfreeze menu
            JustChangedAKey = true;
            return;
        }
        
        if (Control.Interact.IsOnePressed()) // enter in changing mode
        {
            WaitingForKey = true;
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
        {
            spriteBatch.DrawString(Fonts.Font12, Names[i], GetPosition(i), Color.White);
            DrawKeyRectangle(spriteBatch, i);
            //TODO: display current assigned keys
        }
        if (!WaitingForKey)
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

    private static void DrawKeyRectangle(SpriteBatch spriteBatch, int index)
    {
        var x = (int)GetPosition(index).X + 110;
        var y = (int)GetPosition(index).Y - 5;
        var k = Controls[index].KeyCode();
        spriteBatch.Draw( k switch
        {
            >= 65 and <= 90 => Textures.KeysChars[k-65],
            >= 48 and <= 57 => Textures.KeysNums[k-48],
            162 or 163 => Textures.KeysSpecs[0],
            160 or 161 => Textures.KeysSpecs[k-160 + 1],
            164 or 165 => Textures.KeysSpecs[3],
            27 => Textures.KeysSpecs[4],
            13 => Textures.KeysSpecs[5],
            8 => Textures.KeysSpecs[6],
            37 => Textures.KeysSpecs[7],
            39 => Textures.KeysSpecs[8],
            38 => Textures.KeysSpecs[9],
            40 => Textures.KeysSpecs[10],
            9 => Textures.KeysSpecs[11],
            32 => Textures.KeysSpecs[12],
            _ => Textures.KeyVoid
        }, new Vector2(x, y), Colors.White); 
    }

}