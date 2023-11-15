using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Interfaces;

public static class LevelUpMenu
{
    
    private static readonly Rectangle LevelUpSheetArea = new(700, 20, 220, Camera.Height - 40);
    private const int SHeight = 32;
    private static int Sx()
        => LevelUpSheetArea.X + 2;

    private static int Sy(int row)
        => LevelUpSheetArea.Y + (row+3) * SHeight + 3;

    private static int[] _oldValues, _addedValues;
    private static int _newSouls;
    private static int _selectionId;
    private static bool _isLevelUpping;
    
    internal static void Reset()
    {
        _isLevelUpping = true;
        _oldValues = GameScreen.Character.Attributes.GetValues();
        _addedValues = new int[10];
        _newSouls = GameScreen.Character.Souls;
        _selectionId = 1;
    }

    internal static void Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            Sounds.Play(Sounds.IMenuConfirm);
            GameScreen.Character.Souls = _newSouls;
            GameScreen.Character.Attributes.Increase(_addedValues);
            _isLevelUpping = false;
        }
        if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
        {
            Sounds.Play(Sounds.IMenuBack);
            _isLevelUpping = false;
        }
        if (Control.MenuUp.IsOnePressed() && _selectionId > 1)
        {
            Sounds.Play(Sounds.IMenuMove);
            _selectionId--;
        }
        if (Control.MenuDown.IsOnePressed() && _selectionId < _oldValues.Length-2)
        {
            Sounds.Play(Sounds.IMenuMove);
            _selectionId++;
        }
        if (Control.MenuRight.IsOnePressed())
        {
            if (_newSouls < SoulsToLevelUp())
            {
                Sounds.Play(Sounds.IMenuBack);
                return;
            }
            Sounds.Play(Sounds.IMenuMove);
            _newSouls -= SoulsToLevelUp();
            _addedValues[_selectionId]++;
            _addedValues[0]++;
        }
        if (Control.MenuLeft.IsOnePressed() && _addedValues[_selectionId] > 0)
        {
            Sounds.Play(Sounds.IMenuMove);
            _newSouls += SoulsToLevelUp();
            _addedValues[_selectionId]--;
            _addedValues[0]--;
        }
    }

    internal static void Draw()
    {
        var pos = new Vector2(LevelUpSheetArea.X, LevelUpSheetArea.Y);
        Main.SpriteBatch().Draw(Main.PixelTexture(), LevelUpSheetArea, Colors.Black);
        Main.SpriteBatch().Draw(Main.PixelTexture(), new Rectangle(Sx(), Sy(_selectionId), LevelUpSheetArea.Width-8, SHeight), Colors.DarkGray);
        Main.SpriteBatch().DrawString(Fonts.FontBold18, GameScreen.Character.Name, pos + new Vector2(10, 10), Colors.White);
        Main.SpriteBatch().DrawString(Fonts.Font16, "Souls :", pos + new Vector2(10, 40), Colors.White);
        Main.SpriteBatch().DrawString(Fonts.Font18, _newSouls.ToString(), pos + new Vector2(100, 40), Colors.Yellow);
        Main.SpriteBatch().DrawString(Fonts.Font16, "Needed :", pos + new Vector2(10, 60), Colors.White);
        Main.SpriteBatch().DrawString(Fonts.Font18, SoulsToLevelUp().ToString(), pos + new Vector2(100, 60), _newSouls < SoulsToLevelUp() ? Colors.DarkRed : Colors.White);
        for (var i = 0; i < Attributes.NumAttributes; i++)
        {
            var dY = i == 0 ? pos.Y+60 : pos.Y+80 + i*32;
            Main.SpriteBatch().Draw(Textures.IconsAttributes[i], pos + new Vector2(10, dY), Colors.White);
            Main.SpriteBatch().DrawString(Fonts.Font16, Attributes.GetName(i), pos + new Vector2(45, dY+4), Colors.LightGray);
            if (i == _selectionId)
                Main.SpriteBatch().DrawString(Fonts.FontBold18, "< "+DrewValue(i)+" >", pos + new Vector2(170, dY+1), DrewColor(i)); 
            else
                Main.SpriteBatch().DrawString(Fonts.FontBold18, DrewValue(i), pos + new Vector2(182, dY+1), DrewColor(i));
        }
    }
    
    private static int SoulsToLevelUp()
        => GameScreen.Character.Attributes.SoulsToLevelUp(_oldValues[0] + _addedValues[0] + 1);

    private static string DrewValue(int i)
        => (_oldValues[i] + _addedValues[i]).ToString();

    private static Color DrewColor(int i)
        => _addedValues[i] > 0 ?
            Colors.Blue :
            _addedValues[i] < 0 ?
                Colors.DarkRed :
                Colors.White;
    
    public static bool IsLevelUpping()
        => _isLevelUpping;

}