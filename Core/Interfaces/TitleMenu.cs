using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class TitleMenu
{

    private static readonly Vector2 PositionOfTitleList = new (Main.Width/2 - 100, 240);
    private static readonly int HeightOfTitleItem = 28;
    private static readonly Vector2 PositionOfGameList = new (100, 40);
    private static readonly int HeightOfGameItem = 90;
    
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
            GameSelector.Update();
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
        var csx = (int)PositionOfTitleList.X - 4;
        var csy = (int)PositionOfTitleList.Y + _selectionId * HeightOfTitleItem;

        if (_isInGameChoosing || _isInGameChoosingForNew)
        {
            GameSelector.Draw(spriteBatch);
            return;
        }
        
        //spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, 100, HeightOfTitleItem), Color.Orange);
        new RectangleHollow(csx, csy, 100, HeightOfTitleItem, Color.Orange, 2).Draw(spriteBatch);
        for (var i = 0; i < Choices.Length; i++)
            spriteBatch.DrawString(Fonts.Font, Choices[i],  PositionOfTitleList + new Vector2(0, i*HeightOfTitleItem + 4), Color.White);
    }
    
    private static int FirstChoice() => 0;
    private static int LastChoice() => Choices.Length - 1;

    
    
    /**================================================================================================================
     *  GAME SELECTOR SCREEN
     ================================================================================================================*/
    private static class GameSelector
    {
        public static void Update()
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
        }
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            var cgx = (int)PositionOfGameList.X - 8;
            var cgy = (int)PositionOfGameList.Y + _gameSelectedId * HeightOfGameItem - 12;
            
            //spriteBatch.Draw(Main.PixelTexture, new Rectangle(cgx, cgy, 240, HeightOfGameItem), Color.Orange);
            new RectangleHollow(cgx, cgy, 240, HeightOfGameItem, Color.Orange, 4).Draw(spriteBatch);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
            {
                spriteBatch.Draw(SaveSystem.GetGameIcon(i), PositionOfGameList + new Vector2(0, i * HeightOfGameItem - 8), Color.White);
                spriteBatch.DrawString(Fonts.FontBold, SaveSystem.GetGameName(i), PositionOfGameList + new Vector2(70, i * HeightOfGameItem), Color.White);
            }
        }
        
        private static int FirstGameChoice() => 0;
        private static int LastGameChoice() => SaveSystem.SavesCount - 1;
        
    }
    
}