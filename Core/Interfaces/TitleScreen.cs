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

    private static readonly TitleMenu TitleM = new ();
    private static readonly GameSelectionMenu GameSelectionM = new ();
    private static readonly GameCreationMenu GameCreationM = new ();

    public static void Init()
    {
        IsActive = true;
        _activeMenu = TitleM;
    }
    
    //RETURNS: true to QuitApp()
    public static bool Update()
    {
        _activeMenu.Update();
        return _activeMenu == TitleM && TitleM.WantsToQuit;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _activeMenu.Draw(spriteBatch);
    }
    
    
    
    /**================================================================================================================
     *  GAME SELECTOR SCREEN
     *===============================================================================================================*/

    private class TitleMenu : Menu
    {
        
        private static readonly string[] Choices = { "Continue", "Load Game", "New Game", "Settings", "Quit Game" };
        private int _selectionId;
        public bool WantsToQuit;
        
        public TitleMenu()
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
                    case 0: IsActive = false; SaveSystem.Load(); return;
                    case 1: _activeMenu = GameSelectionM; return;
                    case 2: _activeMenu = GameCreationM; return;
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

            //spriteBatch.Draw(Main.PixelTexture, new Rectangle(csx, csy, 100, HeightOfTitleItem), Color.Orange);
            new RectangleHollow(csx, csy, 100, HeightOfTitleItem, Color.Orange, Color.Black, 2).Draw(spriteBatch);
            for (var i = 0; i < Choices.Length; i++)
                spriteBatch.DrawString(Fonts.Font, Choices[i],  PositionOfTitleList + new Vector2(0, i*HeightOfTitleItem + 4), Color.White);
        }
        
        private static int FirstChoice() => 0;
        private static int LastChoice() => Choices.Length - 1;
        
    }
    
    
    
    /**================================================================================================================
     *  GAME SELECTOR SCREEN
     *===============================================================================================================*/
    
    private class GameSelectionMenu : Menu
    {

        private int _selectedGameId;
        private readonly Integer _forNewSave;
        
        public GameSelectionMenu(Integer forNewSave = null)
        {
            _selectedGameId = 0;
            _forNewSave = forNewSave;
        }

        public override void Update()
        {
            if (Controls.Interact.IsOnePressed)
            {
                if (_forNewSave != null)
                    _forNewSave.Value = _selectedGameId;
                else
                {
                    IsActive = false;
                    SaveSystem.Load(_selectedGameId);
                }
                    
            }
            if (Controls.MenuUp.IsOnePressed)
            {
                _selectedGameId--;
                if (_selectedGameId < FirstGameChoice())
                    _selectedGameId = LastGameChoice();
            }
            if (Controls.MenuDown.IsOnePressed)
            {
                _selectedGameId++;
                if (_selectedGameId > LastGameChoice())
                    _selectedGameId = FirstGameChoice();
            }

            if (Controls.MenuBack.IsOnePressed || Controls.Pause.IsOnePressed)
                Init();
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            var cgx = (int)PositionOfGameList.X - 8;
            var cgy = (int)PositionOfGameList.Y + _selectedGameId * HeightOfGameItem - 12;
            
            //spriteBatch.Draw(Main.PixelTexture, new Rectangle(cgx, cgy, 240, HeightOfGameItem), Color.Orange);
            new RectangleHollow(cgx, cgy, 240, HeightOfGameItem, Color.Orange, Color.Black, 4).Draw(spriteBatch);
            for (var i = 0; i < SaveSystem.SavesCount; i++)
            {
                spriteBatch.Draw(SaveSystem.GetGameIcon(i), PositionOfGameList + new Vector2(0, i * HeightOfGameItem - 8), Color.White);
                spriteBatch.DrawString(Fonts.FontBold, SaveSystem.GetGameName(i), PositionOfGameList + new Vector2(70, i * HeightOfGameItem), Color.White);
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
        private readonly TextArea _textArea;
        
        public GameCreationMenu()
        {
            _phase = Phase.PlaceSelection;
            _saveId = new Integer(-1);
            _selectionMenu = new GameSelectionMenu(_saveId);
            _textArea = new TextArea(new Rectangle(100, 100, 100, 28));
        }

        public override void Update()
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    _selectionMenu.Update();
                    if (_saveId.Value != -1)
                        NextPhase();
                    return;
                case Phase.Personalisation:
                    //TODO: character personalisation
                    NextPhase();
                    return;
                case Phase.Name:
                    if (_textArea.Update() == TextArea.States.Confirmed)
                        NextPhase();
                    return;
                case Phase.End:
                    IsActive = false;
                    SaveSystem.CreateNewSave(_saveId.Value, _textArea.Read());
                    return;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (_phase)
            {
                case Phase.PlaceSelection:
                    _selectionMenu.Draw(spriteBatch);
                    return;
                case Phase.Personalisation:
                    //TODO: Draw personalisation screen
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
        
    }
    
}