using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
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

    public static SpriteFont Font, FontBold, FontSoulCounter;
    private static Textures _textures;
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
        Font = Content.Load<SpriteFont>("fonts/font");
        FontBold = Content.Load<SpriteFont>("fonts/font_bold");
        FontSoulCounter = Content.Load<SpriteFont>("fonts/soul_counter");
        _textures = new Textures(Content);
        _world = new World(_textures.BgT);
        _character = new Character(_textures.CharacterDebugT);
        _ath = new Ath(_character, GraphicsDevice);
        LoadMap(Maps.GetMap(101));

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
        for (int y = 0; y < _currentMap.Height; y++)
            for (int x = 0; x < _currentMap.Width; x++)
                if (_currentMap.WallsIds[y][x] != 0)
                    _walls.Add(new Wall(_textures.WallsT[_currentMap.WallsIds[y][x]], x, y));

        _objects = _currentMap.Objects;
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        // KEY TESTS
        Controls.UpdateKeyListener();
        
        MoveCharacter();
        
        if (Controls.KillApp.IsPressed)
            Exit();
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Interact.IsOnePressed)
        {
            foreach (var o in _objects
            .Where(o => o.GetPositionOnGrid() == _character.GetLookingCell()))
                o.Interact(_character);
        }
        if (Controls.Pause.IsPressed)
        {
            _character.AddSouls(3000);
        }
        
        // MODEL UPDATES
        //to do

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        //Add your drawing code here
        _spriteBatch.Begin();
        
        _world.Draw(_spriteBatch);
        foreach (Wall w in _walls) {
            w.Draw(_spriteBatch);
        }
        foreach (InteractiveObject o in _objects) {
            o.Draw(_spriteBatch);
        }
        _character.Draw(_spriteBatch);
        _ath.Draw(_spriteBatch);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
    
    private void MoveCharacter()
    {
        _character.Move(_walls.Concat(_objects).ToList());
    }

}
