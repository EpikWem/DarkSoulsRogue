using System;
using System.Collections.Generic;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Interfaces;

public static class TitleScreen
{

    private static readonly Vector2 PositionOfTitleList = new(Camera.Width/2 - 40, 370);
    private const int TitleItemHeight = 32;
    private const int TitleItemWidth = 100;
    private static readonly Vector2 PositionOfGameList = new(40, 100);
    private const int GameItemHeight = 120;
    private const int GameItemWidth = 240;
    private static readonly Vector2 PositionOfClassList1 = new(40, 90);
    private static readonly Vector2 PositionOfClassList2 = PositionOfClassList1 + new Vector2(ClassItemWidth, -ClassItemHeight*5);
    private const int ClassItemWidth = 210;
    private const int ClassItemHeight = 104;

    private static bool _isActive;
    private static bool _wantsToQuit;
    private static Menu _currentMenu;
    private enum Menu { Title, GameSelection, GameCreation, Settings }

    public static bool IsActive() => _isActive;
    public static void Activate()
    {
        _wantsToQuit = false;
        _isActive = true;
        TitleMenu.Reinit();
        Sounds.Play(Sounds.MTitle);
        _currentMenu = Menu.Title;
    }

    private static void ChangeMenu()
    {
        switch (TitleMenu.SelectionId())
        {
            case 0: _isActive = false; Sounds.StopMusic(); SaveSystem.Load(); return;
            case 1: _currentMenu = Menu.GameSelection; GameSelectionMenu.Reinit(); break;
            case 2: _currentMenu = Menu.GameCreation; GameCreationMenu.Reinit(); break;
            case 3: _currentMenu = Menu.Settings; SettingsMenu.Reinit(); break;
            case 4: _wantsToQuit = true; return;
            default: return;
        }
    }
    
    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        // the go back from settings menu (if settings is not waiting for a key change)
        if (_currentMenu == Menu.Settings && !SettingsMenu.IsWaitingForKey() && (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed()))
        {
            Sounds.Play(Sounds.SMenuBack);
            Activate();
            return false;
        }
        switch (_currentMenu)
        {
            case Menu.Title: TitleMenu.Update(); break;
            case Menu.GameSelection: GameSelectionMenu.Update(); break;
            case Menu.GameCreation: GameCreationMenu.Update(); break;
            case Menu.Settings: SettingsMenu.Update(); break;
        }
        // to quit the game
        return _currentMenu == Menu.Title && _wantsToQuit;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        switch (_currentMenu)
        {
            case Menu.Title: TitleMenu.Draw(spriteBatch); break;
            case Menu.GameSelection: GameSelectionMenu.Draw(spriteBatch); break;
            case Menu.GameCreation: GameCreationMenu.Draw(spriteBatch); break;
            case Menu.Settings: SettingsMenu.Draw(); break;
        }
    }
    
    
    
    /**================================================================================================================
     *  MAIN TITLE MENU
     *===============================================================================================================*/

    private static class TitleMenu
    {
        
        private static readonly string[] Choices = { " Continue", "Load Game", " New Game", "  Settings", "Quit Game" };
        private static int _selectionId;
        
        static TitleMenu() => Reinit();

        //RETURNS: true to QuitApp()
        internal static void Reinit()
            => _selectionId = 0;
        
        internal static void Update()
        {
            if (Control.Interact.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuConfirm);
                ChangeMenu();
            }
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuMove);
                _selectionId = LastChoice();
            }

            if (Control.MenuUp.IsOnePressed() && _selectionId > FirstChoice())
            {
                Sounds.Play(Sounds.SMenuMove);
                _selectionId--;
            }
            if (Control.MenuDown.IsOnePressed() && _selectionId < LastChoice())
            {
                Sounds.Play(Sounds.SMenuMove);
                _selectionId++;
            }
        }

        internal static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.MTitle, Camera.AllScreen, Colors.White);
            var csx = (int)PositionOfTitleList.X - 30;
            var csy = (int)PositionOfTitleList.Y + _selectionId * TitleItemHeight - 2;
            
            spriteBatch.Draw(Main.PixelTexture(), new Rectangle(csx, csy, TitleItemWidth + 40, TitleItemHeight - 4), Colors.Orange);
            for (var i = 0; i < Choices.Length; i++)
                spriteBatch.DrawString(Fonts.Font12, Choices[i],  PositionOfTitleList + new Vector2(0, i*TitleItemHeight + 4), Colors.White);
        }

        internal static int SelectionId() => _selectionId;
        
        private static int FirstChoice() => 0;
        private static int LastChoice() => Choices.Length - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME SELECTION MENU
     *===============================================================================================================*/
    
    private static class GameSelectionMenu
    {

        private static int _selectedGameId;
        private static Integer _forNewSave;
        private static Rectangle _titleRectangle;
        private static string _titleString;

        internal static void Reinit()
        {
            _titleRectangle = new Rectangle(10, 10, 380, 40);
            _titleString = "Select a Game to Load";
            _selectedGameId = 0;
        }
        internal static void Reinit(Integer forNewSave)
        {
            _forNewSave = forNewSave;
            _titleRectangle = new Rectangle(10, 10, 480, 40);
            _titleString = "Select a place for the new Save";
            _selectedGameId = 0;
        }

        internal static void Update()
        {
            if (Control.Interact.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuConfirm);
                if (_forNewSave != null)
                    _forNewSave.Value = _selectedGameId;
                else
                {
                    Sounds.StopMusic();
                    _isActive = false;
                    SaveSystem.Load(_selectedGameId);
                }
            }
            if (Control.MenuUp.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuMove);
                if (_selectedGameId > FirstGameChoice())
                    _selectedGameId--;
            }
            if (Control.MenuDown.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuMove);
                if (_selectedGameId < LastGameChoice())
                    _selectedGameId++;
            }
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
            {
                Sounds.Play(Sounds.SMenuBack);
                Activate();
            }
        }
        
        internal static void Draw(SpriteBatch spriteBatch)
        {
            var cgx = (int)PositionOfGameList.X - 12;
            var cgy = (int)PositionOfGameList.Y + _selectedGameId*GameItemHeight - 12;
            
            spriteBatch.Draw(Main.PixelTexture(), _titleRectangle, Colors.DarkGray);
            spriteBatch.DrawString(Fonts.FontBold24, _titleString, new Vector2(16, 12), Colors.White);
            
            spriteBatch.Draw(Main.PixelTexture(), new Rectangle(cgx, cgy, GameItemWidth, GameItemHeight - 16), Colors.Orange);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
            {
                spriteBatch.Draw(SaveSystem.GetGameIcon(i), PositionOfGameList + new Vector2(0, i * GameItemHeight - 8), Colors.White);
                spriteBatch.DrawString(Fonts.FontBold16, SaveSystem.GetGameName(i), PositionOfGameList + new Vector2(70, i*GameItemHeight), Colors.White);
                spriteBatch.DrawString(Fonts.Font14, "Level " + SaveSystem.GetGameLevel(i), PositionOfGameList + new Vector2(70, i*GameItemHeight + 36), Colors.LightGray);
            }
            
            Attributes.DrawMenuAttributesSheet(SaveSystem.GetGameName(_selectedGameId), SaveSystem.GetGameAttributes(_selectedGameId));
        }
        
        private static int FirstGameChoice() => 0;
        private static int LastGameChoice() => SaveSystem.SavesCount - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME CREATION SCREEN
     *===============================================================================================================*/

    private static class GameCreationMenu
    {

        private enum Phase { PlaceSelection, Personalisation, Name, End }
        private static Phase _phase;
        private static Integer _saveId;

        internal static void Reinit()
        {
            _saveId = new Integer(-1);
            GameSelectionMenu.Reinit(_saveId);
            PersonalisationMenu.Reinit();
            NameArea.Reinit();
            _phase = Phase.PlaceSelection;
        }

        internal static void Update()
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    GameSelectionMenu.Update();
                    if (_saveId.Value != -1)
                        NextPhase();
                    return;
                case Phase.Personalisation:
                    PersonalisationMenu.Update();
                    if (!PersonalisationMenu.IsConfirmed)
                        return;
                    NameArea.Reinit();
                    NextPhase();
                    PersonalisationMenu.IsConfirmed = false;
                    return;
                case Phase.Name:
                    if (Control.Pause.IsOnePressed())
                        _phase = Phase.Personalisation;
                    if (NameArea.Update() == TextArea.States.Confirmed)
                        NextPhase();
                    return;
                case Phase.End:
                    Sounds.StopMusic();
                    _isActive = false;
                    SaveSystem.CreateNewSave(_saveId.Value, NameArea.TextArea.Read(), PersonalisationMenu.GetChosenAttributes(), PersonalisationMenu.GetChosenArmor());
                    return;
            }
        }

        internal static void Draw(SpriteBatch spriteBatch)
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    GameSelectionMenu.Draw(spriteBatch);
                    return;
                case Phase.Personalisation:
                    PersonalisationMenu.Draw(spriteBatch);
                    return;
                case Phase.Name:
                    PersonalisationMenu.Draw(spriteBatch);
                    NameArea.Draw(spriteBatch);
                    return;
                case Phase.End:
                    break;
            }
        }

        private static void NextPhase() =>
            _phase = _phase switch
            {
                Phase.PlaceSelection => Phase.Personalisation,
                Phase.Personalisation => Phase.Name,
                _ => Phase.End
            };

        private static void LastPhase()
        {
            Sounds.Play(Sounds.SMenuBack);
            switch (_phase)
            {
                case Phase.Personalisation:
                    _phase = Phase.PlaceSelection;
                    break;
                case Phase.Name:
                    _phase = Phase.Personalisation;
                    break;
            }
            //=============
            //TODO: fix Go back to last phase 
            //=============
        }

        private static class PersonalisationMenu
        {
            
            private static readonly string[] ClassNames = { "Warrior", "Knight", "Wanderer", "Thief", "Bandit", "Hunter", "Sorcerer", "Pyromancer", "Cleric", "Deprived" };
            private static readonly int[][] BaseAttributesTable = {
                new [] { 4, 11, 8,  12, 13, 13, 11, 9,  9 , 0 },
                new [] { 5, 14, 10, 10, 11, 11, 10, 9,  11, 0 },
                new [] { 3, 10, 11, 10, 10, 14, 12, 11, 8 , 0 },
                new [] { 5, 9,  11, 9,  9,  15, 10, 12, 11, 0 },
                new [] { 4, 12, 8,  14, 14, 9,  11, 8,  10, 0 },
                new [] { 4, 11, 9,  11, 12, 14, 11, 9,  9 , 0 },
                new [] { 3, 8,  15, 8,  9,  11, 8,  15, 8 , 0 },
                new [] { 1, 10, 12, 11, 12, 9,  12, 10, 8 , 0 },
                new [] { 2, 11, 11, 9,  12, 8,  11, 8,  14, 0 },
                new [] { 6, 11, 11, 11, 11, 11, 11, 11, 11, 0 }
            };
            private static readonly Armor[] ClassArmors = {
                Armor.Warrior, Armor.Knight, Armor.Wanderer, Armor.Thief, Armor.Bandit, Armor.Hunter, Armor.Sorcerer, Armor.Pyromancer, Armor.Cleric, Armor.Naked
            };

            private static int _selectedClass;
            internal static bool IsConfirmed;
            private static readonly Rectangle TitleRectangle = new(10, 10, 440, 40);
            private const string _titleString = "Choose your starting Class";

            internal static void Reinit()
            {
                _selectedClass = 0;
                IsConfirmed = false;
            }

            internal static void Update()
            {
                if (Control.Interact.IsOnePressed())
                {
                    Sounds.Play(Sounds.SMenuConfirm);
                    IsConfirmed = true;
                }
                if (Control.MenuUp.IsOnePressed())
                {
                    if (_selectedClass > FirstClassChoice())
                    {
                        Sounds.Play(Sounds.SMenuMove);
                        _selectedClass--;
                    }
                    else
                        Sounds.Play(Sounds.SMenuBack);
                }
                if (Control.MenuDown.IsOnePressed())
                {
                    if (_selectedClass < LastClassChoice())
                    {
                        Sounds.Play(Sounds.SMenuMove);
                        _selectedClass++;
                    }
                    else
                        Sounds.Play(Sounds.SMenuBack);
                }
                if (Control.MenuRight.IsOnePressed())
                {
                    if (_selectedClass < 5)
                    {
                        Sounds.Play(Sounds.SMenuMove);
                        _selectedClass += 5;
                    }
                    else
                        Sounds.Play(Sounds.SMenuBack);
                }
                if (Control.MenuLeft.IsOnePressed())
                {
                    if (_selectedClass >= 5)
                    {
                        Sounds.Play(Sounds.SMenuMove);
                        _selectedClass -= 5;
                    }
                    else
                        Sounds.Play(Sounds.SMenuBack);
                }
                if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                {
                    LastPhase();
                }
            }
        
            internal static void Draw(SpriteBatch spriteBatch)
            {
                var posList = _selectedClass < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                var cgx = (int)posList.X - 8;
                var cgy = (int)posList.Y + _selectedClass * ClassItemHeight - 14;
                
                spriteBatch.Draw(Main.PixelTexture(), TitleRectangle, Colors.DarkGray);
                spriteBatch.DrawString(Fonts.FontBold24, _titleString, new Vector2(16, 12), Colors.White);
                
                spriteBatch.Draw(Main.PixelTexture(), new Rectangle(cgx, cgy, ClassItemWidth, ClassItemHeight+4), Colors.Orange);
                for (var i = 0; i < ClassNames.Length; i++)
                {
                    var posItem = i < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                    spriteBatch.Draw(GetClassIcon(i), posItem + new Vector2(0, i * ClassItemHeight - 8), Colors.White);
                    spriteBatch.DrawString(Fonts.FontBold16, ClassNames[i], posItem + new Vector2(70, i * ClassItemHeight), Colors.White);
                }
                
                Attributes.DrawMenuAttributesSheet(ClassNames[_selectedClass], BaseAttributesTable[_selectedClass]);
            }

            public static List<int> GetChosenAttributes()
                => new(BaseAttributesTable[_selectedClass]) { 0 };

            public static Armor GetChosenArmor()
                => ClassArmors[_selectedClass];

            private static Texture2D GetClassIcon(int index)
                => ClassArmors[index].GetWearingTextures()[1];

            private static int FirstClassChoice()
                => 0;
            private static int LastClassChoice()
                => ClassNames.Length - 1;
            
        }
        
    }

    private static class NameArea
    {

        private static readonly Rectangle BlackRectangle = new(250, 300, 400, 200);
        internal static readonly TextArea TextArea = new(new Rectangle(300, 400, 200, 28));
        
        internal static void Reinit()
            => TextArea.Reinit();

        internal static TextArea.States Update() =>
            TextArea.Update();

        internal static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.PixelTexture(), BlackRectangle, Colors.DarkGrayA);
            TextArea.Draw(spriteBatch);
        }
        
    }
    
}