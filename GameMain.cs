using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DarkSoulsRogue.Core;

namespace DarkSoulsRogue;

public class GameMain : Game
{

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public const int GridWidth = 15, GridHeight = 10, CellSize = 64;
    public const int Width = GridWidth*CellSize, Height = GridHeight*CellSize;

    private World _world;
    private Character _character;
    private List<Wall> _walls = new List<Wall>();
    
    
    
    /**=============================================================
     *= INITIALIZATION METHODS =====================================
     *============================================================*/

    public GameMain()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = Width;
        _graphics.PreferredBackBufferHeight = Height;
    }

    protected override void Initialize()
    {
        _world = new World(LoadTexture("bg"));
        _character = new Character(LoadTexture("character"));
        LoadMap(Maps.UndeadAsylum1);

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
                _walls.Add(new Wall(LoadWallTexture(map.WallsIds[y][x]), x, y));
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
        _character.Draw(_spriteBatch);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
    
    private bool MoveCharacter(Orientation orientation)
    {
        _character.Move(orientation, _walls, Controls.Run.IsPressed);
        return false;
    }
    
    
    
    /**=============================================================
     *= TEXTURES MANAGEMENT ========================================
     *============================================================*/
    
    private Texture2D LoadTexture(string fileName)
    {
        return Content.Load<Texture2D>("images/" + fileName);
    }

    private Texture2D LoadWallTexture(int wallId)
    {
        return LoadTexture("walls/wall" + wallId);
    }
    
}
