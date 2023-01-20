using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DarkSoulsRogue.Core;

namespace DarkSoulsRogue;

public class GameMain : Game
{

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public const int Width = 960, Height = 720;
    
    private World _world;
    private Character _character;
    
    
    
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
        _world = new World(LoadTexture("bg"));
        _character = new Character(LoadTexture("character"));
        
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
            _character.Move(Character.Orientation.Up);
        if (Controls.Down.IsPressed)
            _character.Move(Character.Orientation.Down);
        if (Controls.Right.IsPressed)
            _character.Move(Character.Orientation.Right);
        if (Controls.Left.IsPressed)
            _character.Move(Character.Orientation.Left);
        
        // MODEL UPDATES
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        //Add your drawing code here
        _spriteBatch.Begin();
        _world.Draw(_spriteBatch);
        _character.Draw(_spriteBatch);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
    
    
    
    /**=============================================================
     *= TEXTURES MANAGEMENT ========================================
     *============================================================*/
    
    private Texture2D LoadTexture(string fileName)
    {
        return Content.Load<Texture2D>("images/" + fileName);
    }
    
}
