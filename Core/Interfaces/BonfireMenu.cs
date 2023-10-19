using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class BonfireMenu
{

    private static readonly string[] Choices = { "Level Up", "Reverse Hollowing", "Kindle", "Bottomless Box", "Attune Magic", "Smith" };
    private static int _selectionId;
    private static readonly Rectangle Area = new(40, 40, 200, 400);
    private const int ItemHeight = 30;
    private static bool _isActive;

    public static void Reinit()
    {
        _selectionId = 0;
        _isActive = false;
    }

    public static void Update()
    {
        if (Control.Interact.IsOnePressed())
        {
            Sounds.Play(Sounds.SConfirm);
        }
        
        if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            Deactivate();

        if (Control.MenuUp.IsOnePressed() && _selectionId > FirstChoice())
            _selectionId--;
        if (Control.MenuDown.IsOnePressed() && _selectionId < LastChoice())
            _selectionId++;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, Area, Colors.Black);
        spriteBatch.DrawString(Fonts.FontBold18, "Bonfire", new Vector2(Area.X + 4, Area.Y + 2), Colors.White);
        var csx = Area.X + 4;
        var csy = 30 + Area.Y + _selectionId * ItemHeight - 2;
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, 100, ItemHeight), Colors.Black);
        for (var i = 0; i < Choices.Length; i++)
            spriteBatch.DrawString(Fonts.Font12, Choices[i],  new Vector2(Area.X,  Area.Y + i*ItemHeight + 4), Colors.White);
    }

    public static void Activate()
    {
        if (_isActive)
            return;
        GameScreen.Character.AddLife(GameScreen.Character.MaxLife());
        GameScreen.Character.AddStamina(GameScreen.Character.MaxStamina());
        GameScreen.Character.IsResting = true;
        Reinit();
        Sounds.Play(Sounds.SLit);
        _isActive = true;
    }

    public static void Deactivate()
    {
        if (_isActive)
            return;
        GameScreen.Character.AddLife(GameScreen.Character.MaxLife());
        GameScreen.Character.AddStamina(GameScreen.Character.MaxStamina());
        GameScreen.Character.IsResting = true;
        Reinit();
        Sounds.Play(Sounds.SLit);
        _isActive = true;
    }

    public static bool IsActive() => _isActive;
    
    private static int FirstChoice() => 0;
    private static int LastChoice() => Choices.Length - 1;
    
}