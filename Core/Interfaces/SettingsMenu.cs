using System;
using System.Linq;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DarkSoulsRogue.Core.Interfaces;

public static class SettingsMenu
{
    private static bool _inControlsMenu;

    static SettingsMenu() { Reinit(); }

    internal static void Reinit()
    {
        GeneralMenu.Reinit();
        ControlsMenu.Reinit();
        _inControlsMenu = false;
    }

    private static void SwitchMenu()
    {
        Sounds.Play(Sounds.SMenuMove);
        _inControlsMenu = !_inControlsMenu;
        GeneralMenu.Reinit();
        ControlsMenu.Reinit();
    }

    internal static void Update()
    {
        if ((Control.Consumable.IsOnePressed() || Control.Catalyst.IsOnePressed() || Control.Weapon.IsOnePressed() || Control.Shield.IsOnePressed())
            && !ControlsMenu.WaitingForKey && !ControlsMenu.JustChangedAKey)
            SwitchMenu();
        if (!_inControlsMenu)
            GeneralMenu.Update();
        else
            ControlsMenu.Update();
        if (!ControlsMenu.JustChangedAKey)
            ControlsMenu.JustChangedAKey = false;
    }

    internal static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, IngameMenu.Area, Colors.Black);
        if (!_inControlsMenu)
            GeneralMenu.Draw(spriteBatch);
        else
            ControlsMenu.Draw(spriteBatch);
    }
    
    public static bool IsWaitingForKey() => ControlsMenu.WaitingForKey;

}

internal static class GeneralMenu
{

    private static int[] _levels;
    private static int _selection;
    private static Bar _musicBar, _sfxBar, _ambientBar, _feetBar;

    internal static void Reinit()
    {
        _levels = Sounds.GetLevels();
        _selection = 0;
        _musicBar = new Bar(new Vector2(40, 306), Colors.Orange, 2.0f);
        _sfxBar = new Bar(new Vector2(40, 356), Colors.Orange, 2.0f);
        _ambientBar = new Bar(new Vector2(40, 406), Colors.Orange, 2.0f);
        _feetBar = new Bar(new Vector2(40, 456), Colors.Orange, 2.0f);
    }

    internal static void Update()
    {
        if (Control.MenuUp.IsOnePressed())
        {
            if (_selection > 0)
            {
                Sounds.Play(Sounds.SMenuMove);
                _selection--;
            }
        }
        if (Control.MenuDown.IsOnePressed())
        {
            if (_selection < 3)
            {
                Sounds.Play(Sounds.SMenuMove);
                _selection++;
            }
        }
        if (Control.MenuRight.IsOnePressed())
        {
            if (_levels[_selection] < 100)
            {
                Sounds.Play(Sounds.SMenuMove);
                _levels[_selection] += 10;
                ApplyChanges();
            }
            else
                Sounds.Play(Sounds.SMenuBack);
        }
        if (Control.MenuLeft.IsOnePressed())
        {
            if (_levels[_selection] > 0)
            {
                Sounds.Play(Sounds.SMenuMove);
                _levels[_selection] -= 10;
                ApplyChanges();
            }
            else
                Sounds.Play(Sounds.SMenuBack);
        }
    }

    internal static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(90, 298+50*_selection, 256, 40), Colors.Orange);
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "General Settings", new Vector2(Camera.Width/2 - 260, 100), Colors.White);
        _musicBar.Draw(spriteBatch, Sounds.GetLevel(Sounds.Chanel.Music), 100);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Music).ToString(), new Vector2(300, 300), Colors.White);
        _sfxBar.Draw(spriteBatch, Sounds.GetLevel(Sounds.Chanel.Sfx), 100);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Sfx).ToString(), new Vector2(300, 350), Colors.White);
        _ambientBar.Draw(spriteBatch, Sounds.GetLevel(Sounds.Chanel.Ambient), 100);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Ambient).ToString(), new Vector2(300, 400), Colors.White);
        _feetBar.Draw(spriteBatch, Sounds.GetLevel(Sounds.Chanel.Feet), 100);
        spriteBatch.DrawString(Fonts.Font24, Sounds.GetLevel(Sounds.Chanel.Feet).ToString(), new Vector2(300, 450), Colors.White);
    }

    private static void ApplyChanges()
    {
        Sounds.SetLevels(_levels);
        SettingsSystem.Save();
    }
    
}

internal static class ControlsMenu
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

    internal static bool WaitingForKey;
    internal static bool JustChangedAKey;
    private static RectangleBordered _selection;
    private static int _selectionId;

    static ControlsMenu() => Reinit();
    internal static void Reinit()
    {
        WaitingForKey = false;
        _selection = new RectangleBordered(0, 0, 144, 30, Colors.Orange, Colors.Orange, 2);
        _selectionId = 0;
    }

    internal static void Update()
    {
        if (WaitingForKey) // get a key for replace
        {
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuBack);
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
            Sounds.Play(Sounds.SMenuConfirm);
            return;
        }
        
        if (Control.Interact.IsOnePressed()) // enter in changing mode
        {
            Sounds.Play(Sounds.SMenuConfirm);
            WaitingForKey = true;
            return;
        }
        
        if (Control.MenuUp.IsOnePressed() && _selectionId > FirstChoice())
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId--;
        }
        if (Control.MenuDown.IsOnePressed() && _selectionId < LastChoice())
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId++;
        }
        if (Control.MenuRight.IsOnePressed() && _selectionId < ThirdColumn)
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId += SecondColumn;
        }
        if (Control.MenuLeft.IsOnePressed() && _selectionId >= SecondColumn)
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId -= SecondColumn;
        }

        _selection.SetPosition(GetPosition(_selectionId) + new Vector2(-8, -8));
    }

    internal static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.FontHumanityCounter, "Controls Settings", new Vector2(Camera.Width/2 - 260, 20), Color.White);
        _selection.Draw(spriteBatch);
        for (var i = 0; i < Names.Length; i++)
        {
            spriteBatch.DrawString(Fonts.Font12, Names[i], GetPosition(i), Color.White);
            DrawKeyRectangle(spriteBatch, i);
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