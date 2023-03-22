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
    
    public const int GridWidth = 15, GridHeight = 10, CellSize = 64;
    public const int Width = GridWidth*CellSize, Height = GridHeight*CellSize;
    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static Texture2D PixelTexture;
    
    public static int SaveId;
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
        _graphics.PreferredBackBufferWidth = Width;
        _graphics.PreferredBackBufferHeight = Height;
    }

    protected override void Initialize()
    {
        PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        PixelTexture.SetData(new [] { Color.White });
        Textures.Init(Content);
        Fonts.Init(Content);

        SaveId = 0;
        _background = new Background();
        Character = new Character();
        Ath.Init(Character);
        
        SaveSystem.Init();
        LoadMap(SaveSystem.Load());

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
        // KEY TESTS
        Controls.UpdateKeyListener();
        
        Character.Move(GetCollisionsList());
        Character.TransitMap(Character.TestOutOfMap());

        if (Controls.KillApp.IsPressed)
            QuitApp();
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
            QuitApp();
        }
        if (Controls.Debug1.IsOnePressed)
        {
            Character.ChangeRing(Ring.NoRing);
        }
        if (Controls.Debug2.IsOnePressed)
        {
            Character.ChangeRing(Ring.TinyBeing);
        }
        if (Controls.Debug3.IsOnePressed)
        {
            Character.ChangeRing(Ring.Favor);
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
        
        _background.Draw(_spriteBatch);
        foreach (var w in Walls) {
            w.Draw(_spriteBatch);
        }
        foreach (var o in _objects) {
            o.Draw(_spriteBatch);
        }
        Character.Draw(_spriteBatch);
        Ath.Draw(_spriteBatch);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void QuitApp()
    {
        SaveSystem.Save();
        Exit();
    }

}
