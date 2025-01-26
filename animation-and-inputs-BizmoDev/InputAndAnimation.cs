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

    SpriteEffects flip = SpriteEffects.None; 

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
        _background = Content.Load<Texture2D>("Pac-Man-House"); //background includes black bars on top and bottom

        //setup for the walking sprite
        _walking = new CelAnimationSequence(spriteSheet,(int)182.25, 1 / 5f); //sprite sheet width is 182.25

        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_walking);
    }

    protected override void Update(GameTime gameTime)
    {
        //movement
        KeyboardState direction = Keyboard.GetState();
        bool moving = false;
        
        
        if(direction.IsKeyDown(Keys.Up)) //Move up
        {
            movement.Y -= 5;
            moving = true;
        }
        if(direction.IsKeyDown(Keys.Down)) //Move Down
        {
            movement.Y += 5;
            moving = true;
        }
        if(direction.IsKeyDown(Keys.Left)) //Move Left
        {
            movement.X -= 5;
            flip = SpriteEffects.FlipHorizontally;
            moving = true;
        }
        if(direction.IsKeyDown(Keys.Right)) //Move Right
        {
            movement.X += 5;
            flip = SpriteEffects.None;
            moving = true;
        }

        if (moving)
        {
            _animation01.Update(gameTime);
        }
        else
        {
            _animation01.stop();
            _animation01.reset();
        }
       

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _animation01.Draw(_spriteBatch, movement, flip);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
