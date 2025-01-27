using System.ComponentModel;
using System.Runtime.InteropServices.Marshalling;
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

    private Texture2D _background, _tree;
    private CelAnimationSequence _walking, _coin;
    private CelAnimationPlayer _animation01, _animation02;

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
        //setup for sprites and images
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //setup for the walking sprite
        Texture2D walking = Content.Load<Texture2D>("guy-walking");

        //setup for the coin sprite
        Texture2D coin = Content.Load<Texture2D>("coin");

        //setup for tree
        _tree = Content.Load<Texture2D>("ms_tree");
        //setup for background pac man house
        _background = Content.Load<Texture2D>("Pac-Man-House"); //background includes black bars on top and bottom

        //Walking guy sprite
        _walking = new CelAnimationSequence(walking,(int)182.25, 1 / 5f); //sprite sheet width is 182.25

        //coin sprite
        _coin = new CelAnimationSequence(coin,(int)191.5, 1 / 8f); //sprite sheet width is 191.5

        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_walking);

        _animation02 = new CelAnimationPlayer();
        _animation02.Play(_coin);
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
       
        _animation02.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_tree, new Vector2(250,0), Color.White);
        _animation02.Draw(_spriteBatch, new Vector2(100, 100), SpriteEffects.None);
        _animation01.Draw(_spriteBatch, movement, flip);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
