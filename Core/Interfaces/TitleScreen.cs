using System;
using System.Collections.Generic;
using DarkSoulsRogue.Core.Items;
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
    private static readonly Rectangle AttributesSheetArea = new(620, 20, 320, Camera.Height - 40);

    private static bool _isActive;
    private static Menu _currentMenu;
    private static bool _wantsToQuit;

    private static readonly TitleMenu TitleM = new();
    private static readonly GameSelectionMenu GameSelectionM = new();
    private static readonly GameCreationMenu GameCreationM = new();
    private static readonly SettingsMenu SettingsM = new();

    public static bool IsActive() => _isActive;
    public static void Activate()
    {
        _wantsToQuit = false;
        _isActive = true;
        TitleM.Reinit();
        _currentMenu = TitleM;
    }

    private static void ChangeMenu()
    {
        switch (TitleM.SelectionId())
        {
            case 0: _isActive = false; SaveSystem.Load(); return;
            case 1: _currentMenu = GameSelectionM; break;
            case 2: _currentMenu = GameCreationM; break;
            case 3: _currentMenu = SettingsM; break;
            case 4: _wantsToQuit = true; return;
            default: return;
        }
        _currentMenu.Reinit();
    }
    
    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        if (_currentMenu == SettingsM && !SettingsM.IsWaitingForKey() && (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed()))
        {
            Activate();
            return false;
        }
        _currentMenu.Update();
        return _currentMenu == TitleM && _wantsToQuit;
    }

    public static void Draw(SpriteBatch spriteBatch)
        => _currentMenu.Draw(spriteBatch);
    
    
    
    /**================================================================================================================
     *  MAIN TITLE MENU
     *===============================================================================================================*/

    private class TitleMenu : Menu
    {
        
        private static readonly string[] Choices = { " Continue", "Load Game", " New Game", "  Settings", "Quit Game" };
        private int _selectionId;
        
        internal TitleMenu()
            => Reinit();

        //RETURNS: true to QuitApp()
        internal sealed override void Reinit()
            => _selectionId = 0;
        
        internal override void Update()
        {
            if (Control.Interact.IsOnePressed())
                ChangeMenu();
            if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                _selectionId = LastChoice();

            if (Control.MenuUp.IsOnePressed() && _selectionId > FirstChoice())
                _selectionId--;
            if (Control.MenuDown.IsOnePressed() && _selectionId < LastChoice())
                _selectionId++;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.MTitle, Camera.AllScreen, Colors.White);
            var csx = (int)PositionOfTitleList.X - 30;
            var csy = (int)PositionOfTitleList.Y + _selectionId * TitleItemHeight - 2;
            
            spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, TitleItemWidth + 40, TitleItemHeight - 4), Colors.Orange);
            for (var i = 0; i < Choices.Length; i++)
                spriteBatch.DrawString(Fonts.Font12, Choices[i],  PositionOfTitleList + new Vector2(0, i*TitleItemHeight + 4), Colors.White);
        }

        internal int SelectionId() => _selectionId;
        
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
        private readonly Rectangle _titleRectangle;
        private readonly string _titleString;
        
        internal GameSelectionMenu(Integer forNewSave = null)
        {
            _forNewSave = forNewSave;
            if (_forNewSave == null)
            {
                _titleRectangle = new Rectangle(10, 10, 380, 40);
                _titleString = "Select a Game to Load";
            }
            else
            {
                _titleRectangle = new Rectangle(10, 10, 480, 40);
                _titleString = "Select a place for the new Save";
            }
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
            var cgx = (int)PositionOfGameList.X - 12;
            var cgy = (int)PositionOfGameList.Y + _selectedGameId*GameItemHeight - 12;
            
            spriteBatch.Draw(Main.PixelTexture, _titleRectangle, Colors.DarkGray);
            spriteBatch.DrawString(Fonts.FontBold24, _titleString, new Vector2(16, 12), Colors.White);
            
            spriteBatch.Draw(Main.PixelTexture, new Rectangle(cgx, cgy, GameItemWidth, GameItemHeight - 16), Colors.Orange);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
            {
                spriteBatch.Draw(SaveSystem.GetGameIcon(i), PositionOfGameList + new Vector2(0, i * GameItemHeight - 8), Colors.White);
                spriteBatch.DrawString(Fonts.FontBold16, SaveSystem.GetGameName(i), PositionOfGameList + new Vector2(70, i*GameItemHeight), Colors.White);
                spriteBatch.DrawString(Fonts.Font14, "Level " + SaveSystem.GetGameLevel(i), PositionOfGameList + new Vector2(70, i*GameItemHeight + 36), Colors.LightGray);
            }
            
            DrawAttributesSheet(spriteBatch, SaveSystem.GetGameName(_selectedGameId), SaveSystem.GetGameAttributes(_selectedGameId));
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
        
        internal GameCreationMenu()
        {
            _saveId = new Integer(-1);
            _selectionMenu = new GameSelectionMenu(_saveId);
            _personalisationMenu = new PersonalisationMenu();
            _phase = Phase.PlaceSelection;
        }

        internal sealed override void Reinit()
        {
            _saveId.Value = -1;
            _selectionMenu.Reinit();
            _personalisationMenu.Reinit();
            NameArea.Reinit();
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
                    {
                        NameArea.Reinit();
                        NextPhase();
                        _personalisationMenu.IsConfirmed = false;
                    }
                    return;
                case Phase.Name:
                    if (Control.Pause.IsOnePressed())
                        _phase = Phase.Personalisation;
                    if (NameArea.Update() == TextArea.States.Confirmed)
                        NextPhase();
                    return;
                case Phase.End:
                    _isActive = false;
                    SaveSystem.CreateNewSave(_saveId.Value, NameArea.TextArea.Read(), _personalisationMenu.GetChosenAttributes(), _personalisationMenu.GetChosenArmor());
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
                    _personalisationMenu.Draw(spriteBatch);
                    NameArea.Draw(spriteBatch);
                    return;
                case Phase.End:
                    break;
                default:
                    break;
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

        private class PersonalisationMenu : Menu
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

            private int _selectedClass;
            internal bool IsConfirmed;
            private readonly Rectangle _titleRectangle;
            private readonly string _titleString;

            internal PersonalisationMenu()
            {
                _titleRectangle = new Rectangle(10, 10, 440, 40);
                _titleString = "Choose your starting Class";
                Reinit();
            }

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
                if (Control.MenuRight.IsOnePressed())
                {
                    if (_selectedClass < 5)
                        _selectedClass += 5;
                }
                if (Control.MenuLeft.IsOnePressed())
                {
                    if (_selectedClass >= 5)
                        _selectedClass -= 5;
                }
                if (Control.MenuBack.IsOnePressed() || Control.Pause.IsOnePressed())
                    Activate();
            }
        
            internal override void Draw(SpriteBatch spriteBatch)
            {
                var posList = _selectedClass < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                var cgx = (int)posList.X - 8;
                var cgy = (int)posList.Y + _selectedClass * ClassItemHeight - 14;
                
                spriteBatch.Draw(Main.PixelTexture, _titleRectangle, Colors.DarkGray);
                spriteBatch.DrawString(Fonts.FontBold24, _titleString, new Vector2(16, 12), Colors.White);
                
                spriteBatch.Draw(Main.PixelTexture, new Rectangle(cgx, cgy, ClassItemWidth, ClassItemHeight+4), Colors.Orange);
                for (var i = 0; i < ClassNames.Length; i++)
                {
                    var posItem = i < ClassNames.Length / 2 ? PositionOfClassList1 : PositionOfClassList2;
                    spriteBatch.Draw(GetClassIcon(i), posItem + new Vector2(0, i * ClassItemHeight - 8), Colors.White);
                    spriteBatch.DrawString(Fonts.FontBold16, ClassNames[i], posItem + new Vector2(70, i * ClassItemHeight), Colors.White);
                }
                
                DrawAttributesSheet(spriteBatch, ClassNames[_selectedClass], BaseAttributesTable[_selectedClass]);
            }

            public List<int> GetChosenAttributes()
                => new(BaseAttributesTable[_selectedClass]) { 0 };

            public Armor GetChosenArmor()
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

        internal static TextArea.States Update()
            => TextArea.Update();

        internal static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.PixelTexture, BlackRectangle, Colors.DarkGrayA);
            TextArea.Draw(spriteBatch);
        }
        
    } 

    private static void DrawAttributesSheet(SpriteBatch spriteBatch, string name, int[] values)
    {
        var pos = new Vector2(AttributesSheetArea.X, AttributesSheetArea.Y);
        spriteBatch.Draw(Main.PixelTexture, AttributesSheetArea, Colors.DarkGray);
        spriteBatch.DrawString(Fonts.FontBold18, name, pos + new Vector2(10, 10), Colors.White);
        for (var i = 0; i < Attributes.NumAttributes; i++)
        {
            var dY = i == 0 ? pos.Y + 60 : pos.Y + 80 + i * 32;
            spriteBatch.Draw(Textures.IconsAttributes[i], pos + new Vector2(10, dY), Colors.White);
            spriteBatch.DrawString(Fonts.Font16, Attributes.GetName(i), pos + new Vector2(45, dY+4), Colors.LightGray);
            spriteBatch.DrawString(Fonts.FontBold18, values[i].ToString(), pos + new Vector2(280, dY+1), Colors.White);
        }
    }
    
}