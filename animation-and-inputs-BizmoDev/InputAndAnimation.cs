using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimation : Game
{
    private const int _WindowWidth = 640;
    private const int _WindowHeight = 480;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background, _House;
    private CelAnimationSequence _walking, _sequence02;
    private CelAnimationPlayer _animation01, animation02;

    private Vector2 movement;

    public InputAndAnimation()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D spriteSheet = Content.Load<Texture2D>("guy-walking");

        //setup for background image
        _background = Content.Load<Texture2D>("Pac-Man-House");

        //setup for the walking sprite
        _walking = new CelAnimationSequence(spriteSheet,(int)197.5, 1 / 4f); //sprite sheet width is 197.5

        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_walking);
    }

    protected override void Update(GameTime gameTime)
    {
        //movement
        KeyboardState direction = Keyboard.GetState();
        
        if(direction.IsKeyDown(Keys.Up)) //Move up
            movement.Y -= 5;
        if(direction.IsKeyDown(Keys.Down)) //Move Down
            movement.Y += 5;
        if(direction.IsKeyDown(Keys.Left)) //Move Left
            movement.X -= 5;
        if(direction.IsKeyDown(Keys.Right)) //Move Right
            movement.X += 5;

       _animation01.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _animation01.Draw(_spriteBatch, movement, SpriteEffects.FlipHorizontally);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
