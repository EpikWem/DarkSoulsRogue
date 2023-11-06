using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public class SelectionBar
{

    private const int SWidth = 180, SHeight = 30;

    private readonly string _title;
    private readonly string[] _choices;
    private readonly Rectangle _area;
    
    private bool _isActive;
    private int _selectionId;

    public SelectionBar(string title, string[] choices)
    {
        _title = title;
        _choices = choices;
        _area = new Rectangle(100, 200, SWidth, SHeight * _choices.Length);
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
            _isActive = false;
            return true;
        }
        
        if (Control.MenuBack.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuBack);
            _isActive = false;
        }

        if (Control.MenuUp.IsOnePressed() && _selectionId > 0)
            _selectionId--;
        if (Control.MenuDown.IsOnePressed() && _selectionId < _choices.Length)
            _selectionId++;
        return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, _area, Colors.Black);
        spriteBatch.DrawString(Fonts.FontBold18, _title, new Vector2(_area.X + 4, _area.Y + 2), Colors.White);
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(Sx(), Sy(_selectionId), 100, SHeight), Colors.Black);
        for (var i = 0; i < _choices.Length; i++)
            spriteBatch.DrawString(Fonts.Font12, _choices[i],  new Vector2(Sx() + 2, Sy(i)), Colors.White);
    }

    private int Sx()
        => _area.X + 2;
    private int Sy(int row)
        => _area.Y + (row+1) * SHeight;

}