using System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class TitleScreen
{

    private static readonly Vector2 PositionOfTitleList = new (Camera.Width/2 - 100, 240);
    private static readonly int HeightOfTitleItem = 28;
    private static readonly Vector2 PositionOfGameList = new (100, 40);
    private static readonly int HeightOfGameItem = 90;

    public static bool IsActive;
    private static Menu _activeMenu;
    private static bool _selectForANewGame;

    private static readonly TitleMenu TitleM = new ();
    private static readonly GameSelectionMenu GameSelectionM = new ();
    private static readonly GameCreationMenu GameCreationM = new ();

    public static void Init()
    {
        IsActive = true;
        _activeMenu = TitleM;
        _selectForANewGame = false;
    }
    
    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        _activeMenu.Update();
        return _activeMenu == TitleM && TitleM.WantsToQuit;
    }

    private class TitleMenu : Menu
    {
        
        private readonly string[] _choices = { "Continue", "Load Game", "New Game", "Settings", "Quit Game" };
        private int _selectionId;
        public bool WantsToQuit;
        
        public override void Init()
        {
            _selectionId = 0;
            WantsToQuit = false;
        }

        //RETURNS: true to QuitApp()
        public override void Update()
        {
            if (Controls.Interact.IsOnePressed)
            {
                switch (TitleM._selectionId)
                {
                    case 0: SaveSystem.Load(); return;
                    case 1: _activeMenu = GameSelectionM; return;
                    case 2: _selectForANewGame = true; _activeMenu = GameSelectionM; return;
                    case 3: return;
                    case 4: WantsToQuit = true; return;
                    default: return;
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.PixelTexture, new Rectangle(0, 0, Camera.Width, Camera.Height), Color.Black);
            var csx = (int)PositionOfTitleList.X - 4;
            var csy = (int)PositionOfTitleList.Y + _selectionId * HeightOfTitleItem;

            if (_isInGameChoosing || _isInGameChoosingForNew)
            {
                GameSelectionScreen.Draw(spriteBatch);
                return;
            }
            
            //spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, 100, HeightOfTitleItem), Color.Orange);
            new RectangleHollow(csx, csy, 100, HeightOfTitleItem, Color.Orange, 2).Draw(spriteBatch);
            for (var i = 0; i < _choices.Length; i++)
                spriteBatch.DrawString(Fonts.Font, _choices[i],  PositionOfTitleList + new Vector2(0, i*HeightOfTitleItem + 4), Color.White);
        }
        
        private static int FirstChoice() => 0;
        private static int LastChoice() => _choices.Length - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME SELECTOR SCREEN
     ================================================================================================================*/
    private class GameSelectionMenu : Menu
    {
        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Update()
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
        
        public override void Draw(SpriteBatch spriteBatch)
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

    private class GameCreationMenu : Menu
    {
        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
    
}