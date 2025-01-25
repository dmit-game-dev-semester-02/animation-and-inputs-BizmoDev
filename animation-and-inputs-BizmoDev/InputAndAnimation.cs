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
        Texture2D spriteSheet = Content.Load<Texture2D>("walking");

        //setup for background image
        _background = Content.Load<Texture2D>("Pac-Man-House");

        //setup for the walking sprite
        _walking = new CelAnimationSequence(spriteSheet,105,1/6f); //sprite sheet width is 105.75

        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_walking);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
       _animation01.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _animation01.Draw(_spriteBatch, Vector2.Zero, SpriteEffects.FlipHorizontally);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
