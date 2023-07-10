using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Main : Game
{

    public const bool DrawWalls = true;
    public const bool CameraCentered = false;
    //public const string ContentPath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\";
    public const string ContentPath = @"C:\Users\lucas\Documents\$_DIVERS\Code\CS\DarkSoulsRogue\Content\";

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    public static Texture2D PixelTexture;
    
    public static int CurrentSaveId;
    private static Background _background;
    public static Character Character;
    private static readonly List<Wall> Walls = new();
    private static Map _currentMap;
    
    
    
    /**=============================================================
     *= INITIALIZATION METHODS =====================================
     *============================================================*/

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        _graphics.PreferredBackBufferWidth = Camera.Width;
        _graphics.PreferredBackBufferHeight = Camera.Height;
    }

    protected override void Initialize()
    {
        PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        PixelTexture.SetData(new [] { Color.White });
        
        Textures.Init(Content, GraphicsDevice);
        Fonts.Init(Content);
        Langs.Init(Content);
        SaveSystem.Init();
        SettingsSystem.Init();
        SettingsSystem.Load();
        
        CurrentSaveId = 0;
        _background = new Background();
        Character = new Character("");
        TitleScreen.Activate();
        Ath.Init(Character);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Use this.Content to load your game content here
    }

    public static void LoadMap(int mapId)
    {
        _currentMap = Map.GetMap(mapId);
        Walls.Clear();
        for (var y = 0; y < _currentMap.Height; y++)
            for (var x = 0; x < _currentMap.Width; x++)
                if (_currentMap.WallsIds[y][x] != 0)
                    Walls.Add(new Wall(x, y, Textures.WallsT[_currentMap.WallsIds[y][x]]));
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        Control.UpdateKeyListener(); // key states update
        
        // universal key tests
        if (Control.KillApp.IsPressed())
            Exit(); // kill app with F10
        if (Control.ToggleFullscreen.IsPressed())
            _graphics.ToggleFullScreen();
        if (Control.Debug1.IsOnePressed())
            Character.ChangeArmor(Armor.Crimson);
        if (Control.Debug2.IsOnePressed())
            Character.ChangeArmor(Armor.Artorias);
        if (Control.Debug3.IsOnePressed())
            SettingsSystem.ResetToDefault();

        // update title menu
        if (TitleScreen.IsActive())
        {
            if (TitleScreen.Update())
                Exit();
        }
        
        // update game
        if (!TitleScreen.IsActive())
        {
            // while player is speaking to a Npc, skip character update
            foreach (var entity in _currentMap.Entities.Where(e => e.GetType() == typeof(Npc)))
            {
                var npc = (Npc)entity;
                if (!npc.IsSpeaking)
                    continue;
                npc.Interact(Character);
                return;
            }
            
            // character position updates
            Character.Move(GetCollisionsList());
            Character.TransitMap(Character.TestOutOfMap());

                // update IngameMenu if displayed
            if (IngameMenu.IsActive())
            {
                IngameMenu.Update();
                return;
            }
            // activate menu if pause is pressed
            if (Control.Pause.IsOnePressed())
                IngameMenu.Activate();

            // interactions with InteractiveObjects and Npc 
            if (Control.Interact.IsOnePressed())
            {
                foreach (var o in _currentMap.Objects.Where(o => o.GetHitbox().Contains(Character.GetLookingPoint())))
                    o.Interact(Character);
                foreach (var entity in _currentMap.Entities.Where(e => e.GetType() == typeof(Npc) && e.GetGraphicbox().Contains(Character.GetLookingPoint())))
                    ((Npc)entity).Interact(Character);
            }
            
            // shield key test
            Character.ShieldUp = Control.Shield.IsPressed();
        }

        base.Update(gameTime);
    }

    public static Map CurrentMap() => _currentMap;

    private static List<Rectangle> GetCollisionsList()
    {
        var result = Walls.Select(w => w.GetHitbox()).ToList();
        result.AddRange(_currentMap.Objects
            .Where(
                o => !((o.GetType() == typeof(Door) || o.GetType() == typeof(LockedDoor) || o.GetType() == typeof(OnewayDoor))
                && o.GetState() == 1))
            .Select(w => w.GetHitbox()).ToList());
        result.AddRange(_currentMap.Entities.Select(e => e.GetHitbox()).ToList());
        return result;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        
        // draw title menu
        if (TitleScreen.IsActive())
            TitleScreen.Draw(_spriteBatch);
        
        // or draw game
        else
        {   
            _background.Draw(_spriteBatch);
            if (DrawWalls)
                foreach (var w in Walls)
                    w.Draw(_spriteBatch);
            foreach (var o in _currentMap.Objects)
                o.Draw(_spriteBatch);
            foreach (var e in _currentMap.Entities)
                e.Draw(_spriteBatch);
            Character.Draw(_spriteBatch);
            Ath.Draw(_spriteBatch);
            if (IngameMenu.IsActive())
                IngameMenu.Draw(_spriteBatch);
            ChatBox.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public static void GotoTitle()
    {
        SaveSystem.Save();
        SettingsSystem.Save();
        TitleScreen.Activate();
    }

}
