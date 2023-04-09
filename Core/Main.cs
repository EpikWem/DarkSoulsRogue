using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Narkhedegs.PerformanceMeasurement;

namespace DarkSoulsRogue.Core;

public class Main : Game
{

    public const string ContentPath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\";
    //public const string ContentPath = @"C:\Users\lucas\Documents\$_DIVERS\Code\CS\DarkSoulsRogue\Content\";
    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    public static Texture2D PixelTexture;
    public static readonly Chronometer Chronometer = new ();
    
    public static int CurrentSaveId;
    private static Background _background;
    public static Character Character;
    private static readonly List<Wall> Walls = new ();
    public static Maps.Map CurrentMap;
    
    
    
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
        
        Textures.Init(Content);
        Fonts.Init(Content);
        Langs.Init(Content);
        SaveSystem.Init();
        
        CurrentSaveId = 0;
        _background = new Background();
        Character = new Character("");
        TitleScreen.Init();
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
        CurrentMap = Maps.GetMap(mapId);
        Walls.Clear();
        for (var y = 0; y < CurrentMap.Height; y++)
            for (var x = 0; x < CurrentMap.Width; x++)
                if (CurrentMap.WallsIds[y][x] != 0)
                    Walls.Add(new Wall(x, y, Textures.WallsT[CurrentMap.WallsIds[y][x]]));
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        Controls.UpdateKeyListener(); // key states update
        
        // universal key tests
        if (Controls.KillApp.IsPressed)
            Exit(); // kill app with F10
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Pause.IsOnePressed)
            GotoTitle();
        if (Controls.Debug1.IsOnePressed)
            Character.ChangeShield(Shield.NoShield);
        if (Controls.Debug2.IsOnePressed)
            Character.ChangeShield(Shield.BasicShield);
        if (Controls.Debug3.IsOnePressed)
            Character.ChangeShield(Shield.GrassShield);

        // update title menu
        if (TitleScreen.IsActive)
        {
            if (TitleScreen.Update())
                Exit();
        }
        
        // update game
        else
        {
            // while player is speaking to a Npc, skip character update
            foreach (var entity in CurrentMap.Entities.Where(e => e.GetType() == typeof(Npc)))
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
            
            // interactions with InteractiveObjects and Npc 
            if (Controls.Interact.IsOnePressed)
            {
                foreach (var o in CurrentMap.Objects.Where(o => o.GetPositionOnGrid() == Character.GetLookingCell()))
                {
                    o.Interact(Character);
                }
                foreach (var entity in CurrentMap.Entities.Where(e => e.GetType() == typeof(Npc)))
                {
                    ((Npc)entity).Interact(Character);
                }
            }

            Character.ShieldUp = Controls.Shield.IsPressed; // shield key test

        }

        base.Update(gameTime);
    }

    private static List<Wall> GetCollisionsList() => Walls.Concat(CurrentMap.Objects
            .Where(o => !((o.GetType() == typeof(Door) || o.GetType() == typeof(LockedDoor) || o.GetType() == typeof(OnewayDoor)) && o.GetState() == 1))
            .ToList()).ToList();

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        if (TitleScreen.IsActive)
            TitleScreen.Draw(_spriteBatch); // draw title menu
        else
        {   // draw game
            _background.Draw(_spriteBatch);
            foreach (var w in Walls)
            {
                w.Draw(_spriteBatch);
            }
            foreach (var o in CurrentMap.Objects)
            {
                o.Draw(_spriteBatch);
            }
            Character.Draw(_spriteBatch);
            Ath.Draw(_spriteBatch);
            ChatBox.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private static void GotoTitle()
    {
        SaveSystem.Save();
        TitleScreen.Init();
    }

}
