using System.Collections.Generic;
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

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public const int GridWidth = 15, GridHeight = 10, CellSize = 64;
    public const int Width = GridWidth*CellSize, Height = GridHeight*CellSize;

    
    private static Textures _textures;
    private static Fonts _fonts;
    private static Ath _ath;

    private World _world;
    private Character _character;
    private readonly List<Wall> _walls = new ();
    private List<InteractiveObject> _objects = new ();
    private Maps.Map _currentMap;
    
    
    
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
        _textures = new Textures(Content);
        _fonts = new Fonts(Content);
        _world = new World(Textures.BgT);
        _character = new Character(Armors.Naked.GetWearingTextures());
        _ath = new Ath(_character, GraphicsDevice);
        
        SaveSystem.Init();
        LoadMap(Maps.GetMap(SaveSystem.Load(_character)));
        _character.ChangeArmor(_character.Inventory.EquippedArmor);

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
        
        _walls.Clear();
        for (var y = 0; y < _currentMap.Height; y++)
            for (var x = 0; x < _currentMap.Width; x++)
                if (_currentMap.WallsIds[y][x] != 0)
                    _walls.Add(new Wall(Textures.WallsT[_currentMap.WallsIds[y][x]], x, y));

        _objects = _currentMap.Objects;
        foreach (var o in _objects)
        {
            if (o.GetType() == typeof(Bonfire))
                o.SetTextures(Textures.BonfireT);
            /*if (o.GetType() == typeof(Chest))
                o.SetTextures(_textures.ChestT);*/
            if (o.GetType() == typeof(Door))
                o.SetTextures(Textures.DoorT);
        }
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        // KEY TESTS
        Controls.UpdateKeyListener();
        
        _character.Move(_walls.Concat(_objects).ToList());

        if (Controls.KillApp.IsPressed)
            QuitApp();
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Interact.IsOnePressed)
        {
            foreach (var o in _objects
            .Where(o => o.GetPositionOnGrid() == _character.GetLookingCell()))
            {
                
                if (o.GetType() == typeof(Door) && o.GetState() == 1)
                { // need to pass through the door
                    var d = ((Door)o).Destination;
                    _character.PlaceOnGrid(d);
                    LoadMap(Maps.GetMap(d.MapId));
                }
                else
                    o.Interact(_character);
            }
        }
        if (Controls.Pause.IsOnePressed)
        {
            QuitApp();
        }
        if (Controls.Debug1.IsOnePressed)
        {
            _character.Attributes.Increase(Attributes.Attribute.Humanity, 1);
        }
        if (Controls.Debug2.IsOnePressed)
        {
            _character.ChangeArmor(Armors.Solaire);
        }
        if (Controls.Debug3.IsOnePressed)
        {
            _character.ChangeArmor(Armors.Artorias);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        
        _world.Draw(_spriteBatch);
        foreach (var w in _walls) {
            w.Draw(_spriteBatch);
        }
        foreach (var o in _objects) {
            o.Draw(_spriteBatch);
        }
        _character.Draw(_spriteBatch);
        _ath.Draw(_spriteBatch);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void QuitApp()
    {
        SaveSystem.Save(_currentMap.Id, _character);
        Exit();
    }

}
