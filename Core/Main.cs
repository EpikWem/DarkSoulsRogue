using System;
using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Main : Game
{

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public const int GridWidth = 15, GridHeight = 10, CellSize = 64;
    public const int Width = GridWidth*CellSize, Height = GridHeight*CellSize;

    private Textures _textures;

    private World _world;
    private Character _character;
    private readonly List<Wall> _walls = new ();
    private readonly List<InteractiveObject> _objects = new ();
    
    
    
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
        _world = new World(_textures.BgT);
        _character = new Character(_textures.CharacterDebugT);
        LoadMap(Maps.UndeadAsylum2);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Use this.Content to load your game content here
    }

    private void LoadMap(Maps.Map map)
    {
        for (int y = 0; y < map.Height; y++)
            for (int x = 0; x < map.Width; x++)
                if (map.WallsIds[y][x] != 0)
                    _walls.Add(new Wall(_textures.WallsT[map.WallsIds[y][x]], x, y));
        for (int y = 0; y < map.Height; y++)
            for (int x = 0; x < map.Width; x++)
                if (map.ObjectsIds[y][x] != 0)
                {
                    _objects.Add( map.ObjectsIds[y][x] switch
                    {
                        1 => new Bonfire(_textures.BonfireT, x, y),
                        //2 => new Chest(, x, y),
                        //3 => new Door(, x, y)
                    } );
                }
    }
    
    
    
    /**=============================================================
     *= UPDATES METHODS ============================================
     *============================================================*/
    
    protected override void Update(GameTime gameTime)
    {
        // KEY TESTS
        Controls.UpdateKeyListener();
        if (Controls.KillApp.IsPressed)
            Exit();
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Up.IsPressed)
            MoveCharacter(Orientation.Up);
        if (Controls.Down.IsPressed)
            MoveCharacter(Orientation.Down);
        if (Controls.Right.IsPressed)
            MoveCharacter(Orientation.Right);
        if (Controls.Left.IsPressed)
            MoveCharacter(Orientation.Left);
        if (Controls.Interact.IsOnePressed)
        {
            foreach (InteractiveObject o in _objects)
            {
                if (o.GetPositionOnGrid() == _character.GetLookingCell())
                {
                    o.Interact(_character);
                }
            }
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
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
    
    private bool MoveCharacter(Orientation orientation)
    {
        _character.Move(orientation, _walls.Concat(_objects).ToList(), Controls.Run.IsPressed);
        return false;
    }
    
    
    
    
    
}
