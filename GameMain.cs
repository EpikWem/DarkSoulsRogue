using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    private Wall[] _walls;
    private static GameObject[] _objects;
    
    
    
    /**=============================================================
     *= BASIC METHODS ==============================================
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
        _objects = new GameObject[] {};
        /*_world = new World(LoadTexture("bg"));
        _objects[0] = _world;
        _character = new Character(LoadTexture("character"));
        _objects[1] = _character;*/
        //LoadMap(Maps.UndeadAsylum1);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // KEY TESTS
        Controls.UpdateKeyListener();
        if (Controls.KillApp.IsPressed)
            Exit();
        if (Controls.ToggleFullscreen.IsPressed)
            _graphics.ToggleFullScreen();
        if (Controls.Up.IsPressed)
            _character.Move(Character.Orientation.Up, _walls);
        if (Controls.Down.IsPressed)
            _character.Move(Character.Orientation.Down, _walls);
        if (Controls.Right.IsPressed)
            _character.Move(Character.Orientation.Right, _walls);
        if (Controls.Left.IsPressed)
            _character.Move(Character.Orientation.Left, _walls);
        
        // MODEL UPDATES
        //to do

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        //Add your drawing code here
        _spriteBatch.Begin();
        foreach (GameObject o in _objects) {
            o.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }



    private void LoadMap(Maps.Map map)
    {
        int[] wt = map.WallsIds;
        int wi = 0;
        for (int i = 0; i < wt.Length; i++)
        {
            if (wt[i] != 0)
            {
                _walls[wi] = new Wall(
                    LoadWallTexture(i),
                    i % GridWidth,
                    (int)(i / GridWidth));
                wi++;
            }
        }
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
        return LoadTexture("walls/wall1");
    }
    
}
