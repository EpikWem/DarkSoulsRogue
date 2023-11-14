using System;
using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Utilities;

public class Attributes
{
        
    private static readonly Rectangle MenuAttributesSheetArea = new(700, 20, 220, Camera.Height - 40);
    
    public enum Attribute { Level, Vitality, Attunement, Endurance, Strength, Dexterity, Resistance, Intelligence, Faith, Humanity }

    public const int NumAttributes = 10;
    
    private readonly int[] _values = new int[NumAttributes];

    public Attributes()
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = 1;
    }
    
    public int Get(Attribute attribute) => _values[(int)attribute];

    public void Set(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = values[i];
    }
    public void Set(List<int> values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] = values[i];
    }
    
    public void Increase(Attribute attribute, int value)
    {
        _values[(int)attribute] += value;
    }
    
    public void Increase(int[] values)
    {
        for (var i = 0; i < NumAttributes; i++)
            _values[i] += values[i];
    }

    public int SoulsToLevelUp(int newLevel)
    {
        return newLevel switch
        {
            2 => 673,
            3 => 690,
            4 => 707,
            5 => 724,
            6 => 741,
            7 => 758,
            8 => 775,
            9 => 793,
            10 => 811,
            11 => 829,
            >= 12 => 1 + (int)(0.02f * Math.Pow((float)newLevel, 3) + 3.06f * Math.Pow((float)newLevel, 2) + 105.6f * newLevel - 895),
            _ => 1
        };
    }

    public int[] GetValues()
    {
        var result = new int[10];
        for (var i = 0; i < _values.Length; i++)
            result[i] = _values[i];
        return result;
    }


    public static string GetName(int index)
        => index switch { 
            0 => "Level",
            1 => "Vitality",
            2 => "Attunement",
            3 => "Endurance",
            4 => "Strength",
            5 => "Dexterity",
            6 => "Resistance",
            7 => "Intelligence",
            8 => "Faith",
            9 => "Humanity",
            _ => "<UNKNOWN>"
        };
    
    public static void DrawMenuAttributesSheet(string name, int[] values)
    {
        var pos = new Vector2(MenuAttributesSheetArea.X, MenuAttributesSheetArea.Y);
        Main.SpriteBatch().Draw(Main.PixelTexture(), MenuAttributesSheetArea, Colors.DarkGray);
        Main.SpriteBatch().DrawString(Fonts.FontBold18, name, pos + new Vector2(10, 10), Colors.White);
        for (var i = 0; i < NumAttributes; i++)
        {
            var dY = i == 0 ? pos.Y + 60 : pos.Y + 80 + i * 32;
            Main.SpriteBatch().Draw(Textures.IconsAttributes[i], pos + new Vector2(10, dY), Colors.White);
            Main.SpriteBatch().DrawString(Fonts.Font16, GetName(i), pos + new Vector2(45, dY+4), Colors.LightGray);
            Main.SpriteBatch().DrawString(Fonts.FontBold18, values[i].ToString(), pos + new Vector2(182, dY+1), Colors.White);
        }
    }

}