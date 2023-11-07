using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public class SelectionBar
{

    private const int SWidth = 180, SHeight = 24;

    private readonly string _title;
    private readonly string[] _choices;
    private readonly Rectangle _area;
    
    private bool _isActive;
    private int _selectionId;

    public SelectionBar(string title, string[] choices)
    {
        _title = title;
        _choices = choices;
        _area = new Rectangle(100, 200, SWidth, SHeight*(_choices.Length+1) + 4);
        Reset();
        _isActive = false;
    }
    
    public void Reset()
    {
        _isActive = true;
        _selectionId = 0;
    }

    public bool IsActive()
        => _isActive;
    public int SelectionId()
        => _selectionId;

    /** 
     * Returns TRUE if player Confirmed his choice.
     */
    public bool Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuConfirm);
            return true;
        }
        if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuBack);
            _isActive = false;
        }
        if (Control.MenuUp.IsOnePressed() && _selectionId > 0)
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId--;
        }
        if (Control.MenuDown.IsOnePressed() && _selectionId < _choices.Length-1)
        {
            Sounds.Play(Sounds.SMenuMove);
            _selectionId++;
        }
        return false;
    }

    public void Draw()
    {
        Main.SpriteBatch.Draw(Main.PixelTexture, _area, Colors.Black);
        Main.SpriteBatch.DrawString(Fonts.FontBold18, _title, new Vector2(_area.X + 6, _area.Y + 2), Colors.White);
        Main.SpriteBatch.Draw(Main.PixelTexture, new Rectangle(Sx(), Sy(_selectionId), 172, SHeight), Colors.Orange);
        for (var i = 0; i < _choices.Length; i++)
            Main.SpriteBatch.DrawString(Fonts.Font12, _choices[i],  new Vector2(Sx() + 6, Sy(i)+4), Colors.LightGray);
    }

    private int Sx()
        => _area.X + 2;
    private int Sy(int row)
        => _area.Y + (row+1) * SHeight;

}