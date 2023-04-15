using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotImplementedException = System.NotImplementedException;

namespace DarkSoulsRogue.Core.Interfaces;

public static class TitleScreen
{

    private static readonly Vector2 PositionOfTitleList = new(Camera.Width/2 - 100, 240);
    private const int TitleItemHeight = 28;
    private const int TitleItemWidth = 100;
    private static readonly Vector2 PositionOfGameList = new(100, 40);
    private const int GameItemHeight = 90;
    private const int GameItemWidth = 240;
    private static readonly Vector2 PositionOfClassList1 = new(100, 40);
    private static readonly Vector2 PositionOfClassList2 = PositionOfClassList1 + new Vector2(ClassItemWidth, -ClassItemHeight*5);
    private const int ClassItemHeight = 90;
    private const int ClassItemWidth = 240;

    private static bool _isActive;
    private static Menu _currentMenu;

    private static readonly TitleMenu TitleM = new();
    private static readonly GameSelectionMenu GameSelectionM = new();
    private static readonly GameCreationMenu GameCreationM = new();

    public static bool IsActive() => _isActive;
    public static void Activate()
    {
        _isActive = true;
        ChangeMenu(TitleM);
    }

    private static void ChangeMenu(Menu menu)
    {
        _currentMenu = menu;
        _currentMenu.Reinit();
    }
    
    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        _currentMenu.Update();
        return _currentMenu == TitleM && TitleM.WantsToQuit;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _currentMenu.Draw(spriteBatch);
    }
    
    
    
    /**================================================================================================================
     *  MAIN TITLE MENU
     *===============================================================================================================*/

    private class TitleMenu : Menu
    {
        
        private static readonly string[] Choices = { "Continue", "Load Game", "New Game", "Settings", "Quit Game" };
        private int _selectionId;
        public bool WantsToQuit;
        
        internal TitleMenu()
        {
            Reinit();
        }

        //RETURNS: true to QuitApp()
        internal sealed override void Reinit()
        {
            _selectionId = 0;
            WantsToQuit = false;
        }
        
        internal override void Update()
        {
            if (Control.Interact.IsOnePressed())
            {
                switch (TitleM._selectionId)
                {
                    case 0: _isActive = false; SaveSystem.Load(); return;
                    case 1: ChangeMenu(GameSelectionM); return;
                    case 2: ChangeMenu(GameCreationM); return;
                    case 3: return;
                    case 4: WantsToQuit = true; return;
                    default: return;
                }
            }

            if (Control.MenuUp.IsOnePressed())
            {
                if (_selectionId > FirstChoice())
                    _selectionId--;
            }

            if (Control.MenuDown.IsOnePressed())
            {
                if (_selectionId < LastChoice())
                    _selectionId++;
            }

            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                _selectionId = LastChoice();
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.PixelTexture, new Rectangle(0, 0, Camera.Width, Camera.Height), Color.Black);
            var csx = (int)PositionOfTitleList.X - 4;
            var csy = (int)PositionOfTitleList.Y + _selectionId * TitleItemHeight;

            //spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, 100, HeightOfTitleItem), Color.Orange);
            new RectangleHollow(csx, csy, TitleItemWidth, TitleItemHeight, Color.Orange, Color.Black, 2).Draw(spriteBatch);
            for (var i = 0; i < Choices.Length; i++)
                spriteBatch.DrawString(Fonts.Font, Choices[i],  PositionOfTitleList + new Vector2(0, i*TitleItemHeight + 4), Color.White);
        }
        
        private static int FirstChoice() => 0;
        private static int LastChoice() => Choices.Length - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME SELECTION MENU
     *===============================================================================================================*/
    
    private class GameSelectionMenu : Menu
    {

        private int _selectedGameId;
        private readonly Integer _forNewSave;
        
        internal GameSelectionMenu(Integer forNewSave = null)
        {
            _forNewSave = forNewSave;
            Reinit();
        }

        internal sealed override void Reinit()
        {
            _selectedGameId = 0;
        }

        internal override void Update()
        {
            if (Control.Interact.IsOnePressed())
            {
                if (_forNewSave != null)
                    _forNewSave.Value = _selectedGameId;
                else
                {
                    _isActive = false;
                    SaveSystem.Load(_selectedGameId);
                }
                    
            }
            if (Control.MenuUp.IsOnePressed())
            {
                if (_selectedGameId > FirstGameChoice())
                    _selectedGameId--;
            }
            if (Control.MenuDown.IsOnePressed())
            {
                if (_selectedGameId < LastGameChoice())
                    _selectedGameId++;
            }

            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                Activate();
        }
        
        internal override void Draw(SpriteBatch spriteBatch)
        {
            var cgx = (int)PositionOfGameList.X - 8;
            var cgy = (int)PositionOfGameList.Y + _selectedGameId * GameItemHeight - 12;
            
            new RectangleHollow(cgx, cgy, GameItemWidth, GameItemHeight, Color.Orange, Color.Black, 4).Draw(spriteBatch);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
            {
                spriteBatch.Draw(SaveSystem.GetGameIcon(i), PositionOfGameList + new Vector2(0, i * GameItemHeight - 8), Color.White);
                spriteBatch.DrawString(Fonts.FontBold, SaveSystem.GetGameName(i), PositionOfGameList + new Vector2(70, i * GameItemHeight), Color.White);
            }
        }
        
        private static int FirstGameChoice() => 0;
        private static int LastGameChoice() => SaveSystem.SavesCount - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME CREATION SCREEN
     *===============================================================================================================*/

    private class GameCreationMenu : Menu
    {

        private enum Phase { PlaceSelection, Personalisation, Name, End }
        private Phase _phase;
        private readonly Integer _saveId;
        private readonly GameSelectionMenu _selectionMenu;
        private readonly PersonalisationMenu _personalisationMenu;
        private readonly TextArea _textArea;
        
        internal GameCreationMenu()
        {
            _saveId = new Integer(-1);
            _selectionMenu = new GameSelectionMenu(_saveId);
            _personalisationMenu = new PersonalisationMenu();
            _textArea = new TextArea(new Rectangle(100, 100, 100, 28));
            Reinit();
        }

        internal sealed override void Reinit()
        {
            _phase = Phase.PlaceSelection;
        }

        internal override void Update()
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    _selectionMenu.Update();
                    if (_saveId.Value != -1)
                        NextPhase();
                    return;
                case Phase.Personalisation:
                    _personalisationMenu.Update();
                    if (_personalisationMenu.IsConfirmed)
                        NextPhase();
                    return;
                case Phase.Name:
                    if (_textArea.Update() == TextArea.States.Confirmed)
                        NextPhase();
                    return;
                case Phase.End:
                    _isActive = false;
                    SaveSystem.CreateNewSave(_saveId.Value, _textArea.Read(), _personalisationMenu.GetChosenAttributes());
                    return;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    _selectionMenu.Draw(spriteBatch);
                    return;
                case Phase.Personalisation:
                    _personalisationMenu.Draw(spriteBatch);
                    return;
                case Phase.Name:
                    _textArea.Draw(spriteBatch);
                    return;
            }
        }

        private void NextPhase()
        {
            _phase = _phase switch
            {
                Phase.PlaceSelection => Phase.Personalisation,
                Phase.Personalisation => Phase.Name,
                _ => Phase.End
            };
        }
        private void PastPhase()
        {
            if (_phase == Phase.PlaceSelection)
                Activate();
            _phase = _phase switch
            {
                Phase.Personalisation => Phase.PlaceSelection,
                Phase.Name => Phase.Personalisation
            };
        }

        private class PersonalisationMenu : Menu
        {
            
            private static readonly string[] ClassNames = { "Warrior", "Knight", "Wanderer", "Thief", "Bandit", "Hunter", "Sorcerer", "Pyromancer", "Cleric", "Deprived" };
            private static readonly int[][] BaseAttributesTable = {
                new [] { 11, 8,  12, 13, 13, 11, 9,  9  },
                new [] { 14, 10, 10, 11, 11, 10, 9,  11 },
                new [] { 10, 11, 10, 10, 14, 12, 11, 8  },
                new [] { 9,  11, 9,  9,  15, 10, 12, 11 },
                new [] { 12, 8,  14, 14, 9,  11, 8,  10 },
                new [] { 11, 9,  11, 12, 14, 11, 9,  9  },
                new [] { 8,  15, 8,  9,  11, 8,  15, 8  },
                new [] { 10, 12, 11, 12, 9,  12, 10, 8  },
                new [] { 11, 11, 9,  12, 8,  11, 8,  14 },
                new [] { 11, 11, 11, 11, 11, 11, 11, 11 }
            };
            private static readonly Texture2D[] ClassIcons = { Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0], Textures.ArmorTextures[0][0] };

            private int _selectedClass;
            internal bool IsConfirmed;

            internal PersonalisationMenu() => Reinit();

            internal sealed override void Reinit()
            {
                _selectedClass = 0;
                IsConfirmed = false;
            }

            internal override void Update()
            {
                if (Control.Interact.IsOnePressed())
                {
                    IsConfirmed = true;
                }
                if (Control.MenuUp.IsOnePressed())
                {
                    if (_selectedClass > FirstClassChoice())
                        _selectedClass--;
                }
                if (Control.MenuDown.IsOnePressed())
                {
                    if (_selectedClass < LastClassChoice())
                        _selectedClass++;
                }
                if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                    Activate();
            }
        
            internal override void Draw(SpriteBatch spriteBatch)
            {
                var posList = _selectedClass < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                var cgx = (int)posList.X - 8;
                var cgy = (int)posList.Y + _selectedClass * ClassItemHeight - 12;
            
                new RectangleHollow(cgx, cgy, ClassItemWidth, ClassItemHeight, Color.Orange, Color.Black, 4).Draw(spriteBatch);
                for (var i = 0; i < ClassNames.Length; i++)
                {
                    var posItem = i < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                    spriteBatch.Draw(ClassIcons[i], posItem + new Vector2(0, i * ClassItemHeight - 8), Color.White);
                    spriteBatch.DrawString(Fonts.FontBold, ClassNames[i], posItem + new Vector2(70, i * ClassItemHeight), Color.White);
                }
            }

            public List<int> GetChosenAttributes() => new(BaseAttributesTable[_selectedClass]) { 0 };
        
            private static int FirstClassChoice() => 0;
            private static int LastClassChoice() => ClassNames.Length - 1;
            
        }
        
    }
    
}