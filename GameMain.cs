using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DarkSoulsRogue.Core;

namespace DarkSoulsRogue;

public class GameMain : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    public const int WIDTH = 960, HEIGHT = 720;
    World world;

    public GameMain()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        graphics.PreferredBackBufferWidth = WIDTH;
        graphics.PreferredBackBufferHeight = HEIGHT;
    }

    protected override void Initialize()
    {
        //Add your initialization logic here
        world = new World();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        //Use this.Content to load your game content here
        world.Texture = Content.Load<Texture2D>("images/bg");
        world.Position = new Vector2(0, 0);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KeyPressed(Keys.F10))
            Exit();
        if (KeyPressed(Keys.F11))
            graphics.ToggleFullScreen();

        //Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        //Add your drawing code here
        spriteBatch.Begin();
        world.Draw(spriteBatch);
        spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private bool KeyPressed(Keys key)
    {
        return Keyboard.GetState().IsKeyDown(key);
    }
    
    private bool KeyReleased(Keys key)
    {
        return Keyboard.GetState().IsKeyUp(key);
    }
    
}
