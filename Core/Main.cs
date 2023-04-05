using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Main : Game
{
    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static Texture2D PixelTexture;
    
    public static int CurrentSaveId;
    private static Background _background;
    public static Character Character;
    private static readonly List<Wall> Walls = new ();
    private static List<InteractiveObject> _objects = new ();
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

        SaveSystem.Init();
        
        CurrentSaveId = 0;
        _background = new Background();
        Character = new Character("");
        Ath.Init(Character);
        TitleScreen.Init();

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

        _objects = CurrentMap.Objects;
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        Controls.UpdateKeyListener(); // key states update
        
        if (Controls.KillApp.IsPressed)
            Exit(); // kill app with F10
        
        if (TitleScreen.IsActive)
            if (TitleScreen.Update()) // update title menu
                Exit();
        if (!TitleScreen.IsActive)
        {   // update game
            Character.Move(GetCollisionsList());
            Character.TransitMap(Character.TestOutOfMap());
            
            if (Controls.ToggleFullscreen.IsPressed)
                _graphics.ToggleFullScreen();
            if (Controls.Interact.IsOnePressed)
            {
                foreach (var o in _objects
                             .Where(o => o.GetPositionOnGrid() == Character.GetLookingCell()))
                {
                    o.Interact(Character);
                    //TODO: Remove doors from walls when they are opened
                }
            }

            if (Controls.Pause.IsOnePressed)
            {
                GotoTitle();
            }

            if (Controls.Debug1.IsOnePressed)
            {
                
            }

            if (Controls.Debug2.IsOnePressed)
            {

            }

            if (Controls.Debug3.IsOnePressed)
            {

            }
        }

        base.Update(gameTime);
    }

    private static List<Wall> GetCollisionsList()
    {
        var objs = _objects
            .Where(o => !((o.GetType() == typeof(Door) || o.GetType() == typeof(LockedDoor) || o.GetType() == typeof(OnewayDoor)) && o.GetState() == 1)).ToList();
        return Walls.Concat(objs).ToList();
    }

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
            foreach (var o in _objects)
            {
                o.Draw(_spriteBatch);
            }
            Character.Draw(_spriteBatch);
            Ath.Draw(_spriteBatch);
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
