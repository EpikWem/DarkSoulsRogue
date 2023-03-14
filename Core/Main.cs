﻿using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items.Lists;
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
    
    private static Background _background;
    public static Character Character;
    private static readonly List<Wall> Walls = new ();
    private static List<InteractiveObject> _objects = new ();
    private static Maps.Map _currentMap;
    
    
    
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
        
        _background = new Background();
        Character = new Character();
        Ath.Init(Character);
        
        SaveSystem.Init();
        LoadMap(Maps.GetMap(SaveSystem.Load(Character)));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Use this.Content to load your game content here
    }

    private void LoadMap(Maps.Map map)
    {
        _currentMap = map;
        
        Walls.Clear();
        for (var y = 0; y < _currentMap.Height; y++)
            for (var x = 0; x < _currentMap.Width; x++)
                if (_currentMap.WallsIds[y][x] != 0)
                    Walls.Add(new Wall(x, y, Textures.WallsT[_currentMap.WallsIds[y][x]]));

        _objects = _currentMap.Objects;
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        // KEY TESTS
        Controls.UpdateKeyListener();
        
        Character.Move(Walls.Concat(_objects).ToList());

        if (Controls.KillApp.IsPressed)
            QuitApp();
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Interact.IsOnePressed)
        {
            foreach (var o in _objects
            .Where(o => o.GetPositionOnGrid() == Character.GetLookingCell()))
            {
                
                if (o.GetType() == typeof(Door) && o.GetState() == 1)
                { // need to pass through the door
                    var d = ((Door)o).Destination;
                    Character.PlaceOnGrid(d);
                    LoadMap(Maps.GetMap(d.MapId));
                }
                else
                    o.Interact(Character);
            }
        }
        if (Controls.Pause.IsOnePressed)
        {
            QuitApp();
        }
        if (Controls.Debug1.IsOnePressed)
        {
            Character.ChangeRing(Rings.NoRingR);
        }
        if (Controls.Debug2.IsOnePressed)
        {
            Character.ChangeRing(Rings.TinyBeingR);
        }
        if (Controls.Debug3.IsOnePressed)
        {
            Character.ChangeRing(Rings.CloranthyR);
        }

        base.Update(gameTime);
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
        SaveSystem.Save(_currentMap.Id, Character);
        Exit();
    }

}
