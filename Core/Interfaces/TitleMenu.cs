using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class TitleMenu
{

    private static readonly Vector2 PositionOfList = new (100, 100);
    
    private static readonly string[] Choices = { "Continue", "Load Game", "New Game", "Settings", "Quit Game" };

    private static int _selectionId, _gameSelectedId;
    public static bool IsActive;
    private static bool _isInGameChoosing, _isInGameChoosingForNew;

    public static void Init()
    {
        _selectionId = 0;
        _gameSelectedId = 0;
        IsActive = true;
        _isInGameChoosing = false;
        _isInGameChoosingForNew = false;
    }

    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        if (_isInGameChoosing || _isInGameChoosingForNew)
        {
            if (Controls.Interact.IsOnePressed)
            {
                if (_isInGameChoosing)
                    SaveSystem.Load(_gameSelectedId);
                if (_isInGameChoosingForNew)
                    SaveSystem.CreateNewSave(_gameSelectedId);
            }
            if (Controls.MenuUp.IsOnePressed)
            {
                _gameSelectedId--;
                if (_gameSelectedId < FirstGameChoice())
                    _gameSelectedId = LastGameChoice();
            }
            if (Controls.MenuDown.IsOnePressed)
            {
                _gameSelectedId++;
                if (_gameSelectedId > LastGameChoice())
                    _gameSelectedId = FirstGameChoice();
            }

            if (Controls.MenuBack.IsOnePressed || Controls.Pause.IsOnePressed)
                Init();
            
            return false;
        }
        if (Controls.Interact.IsOnePressed)
        {
            switch (_selectionId)
            {
                case 0:
                    SaveSystem.Load();
                    return false;
                case 1:
                    _isInGameChoosing = true;
                    return false;
                case 2:
                    _isInGameChoosingForNew = true;
                    return false;
                case 3: return false;
                case 4: return true;
                default: return false;
            }
        }

        if (Controls.MenuUp.IsOnePressed)
        {
            _selectionId--;
            if (_selectionId < FirstChoice())
                _selectionId = LastChoice();
        }

        if (Controls.MenuDown.IsOnePressed)
        {
            _selectionId++;
            if (_selectionId > LastChoice())
                _selectionId = FirstChoice();
        }

        if (Controls.MenuBack.IsOnePressed || Controls.Pause.IsOnePressed)
            _selectionId = LastChoice();
        
        return false;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(0, 0, Main.Width, Main.Height), Color.Black);
        var tcpx = (int)PositionOfList.X;
        var tcpy = (int)(PositionOfList + new Vector2(0, _selectionId * 30)).Y;
        var gcpy = (int)(PositionOfList + new Vector2(0, _gameSelectedId * 60)).Y;

        if (_isInGameChoosing || _isInGameChoosingForNew)
        {
            spriteBatch.Draw(Main.PixelTexture, new Rectangle(tcpx, gcpy, 100, 20), Color.Orange);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
                spriteBatch.DrawString(Fonts.FontBold, "Save n"+ i,  PositionOfList + new Vector2(0, i*60), Color.White);
            return;
        }
        
        spriteBatch.Draw(Main.PixelTexture, new Rectangle(tcpx, tcpy, 100, 20), Color.Orange);
        for (var i = 0; i < Choices.Length; i++)
            spriteBatch.DrawString(Fonts.Font, Choices[i],  PositionOfList + new Vector2(0, i*30), Color.White);
    }
    
    private static int FirstChoice() => 0;
    private static int LastChoice() => Choices.Length - 1;
    private static int FirstGameChoice() => 0;
    private static int LastGameChoice() => SaveSystem.SavesCount - 1;

}