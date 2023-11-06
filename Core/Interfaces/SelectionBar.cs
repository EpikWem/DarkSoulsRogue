using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public class SelectionMenu
{

    private const int SWidth = 180, SHeight = 30;

    private readonly string _title;
    private readonly string[] _choices;
    private readonly Rectangle _area;
    
    private int _selectionId;

    public SelectionMenu(string title, string[] choices)
    {
        _title = title;
        _choices = choices;
        _area = new (100, 200, SWidth, SHeight*_choices.Length)
        Reset();
    }
    
    public void Reset()
        => _selectionId = 0;

    public int SelectionId()
        => _selectionId;

    /** 
     * Returns TRUE if player Confirmed his choice.
     * Needs to implement the GoBack function out of this object.
     */
    public bool Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            Sounds.Play(Sounds.SMenuConfirm);
            return true;
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