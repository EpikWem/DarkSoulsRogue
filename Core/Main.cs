using DarkSoulsRogue.Core.GameObjects.Entities;
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
    
    public static Texture2D PixelTexture;
    public static SpriteBatch SpriteBatch;
    
    
    
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
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        Consumable.Init(GameScreen.Character);
        Textures.Init(Content, GraphicsDevice);
        Fonts.Init(Content);
        Langs.Init(Content);
        Sounds.Init(Content);
        SaveSystem.Init();
        SettingsSystem.Init();
        SettingsSystem.Load();
        GameScreen.Init();
        
        TitleScreen.Activate();
        Ath.Init(GameScreen.Character);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        //Use this.Content to load your game content here
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
        
        // debug controls
        if (Control.Debug1.IsOnePressed())
            ;
        if (Control.Debug2.IsOnePressed())
            ;
        if (Control.Debug3.IsOnePressed())
            ;

        // update title menu
        if (TitleScreen.IsActive())
        {
            if (TitleScreen.Update())
                Exit();
        }
        else // update game
        {
            GameScreen.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin();
        
        // draw title menu
        if (TitleScreen.IsActive())
            TitleScreen.Draw(SpriteBatch);

        // or draw game
        else
            GameScreen.Draw();
        
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    public static void GotoTitle()
    {
        SaveSystem.Save();
        SettingsSystem.Save();
        TitleScreen.Activate();
    }

}
