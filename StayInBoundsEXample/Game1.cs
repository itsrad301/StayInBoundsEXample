using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace StayInBoundsEXample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _txBackground;

        private Texture2D _texture2D;
        private Rectangle _textureRect;
        private Vector2 _txPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the texture to be drawn using the rectangle
            _texture2D = Content.Load<Texture2D>(@"body");
            // the rectangle is the size of the image bounds which is a rectangle
            // Bounds is Rectangle(0,0,image.Width,image.Height)
            _textureRect = _texture2D.Bounds;
            // The position of the rectangle
            _txPosition = new Vector2(100, 100);
            // translate the rectnagle from 0,0 to 100,100 to begin
            _textureRect.Offset(_txPosition.ToPoint());

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Remmber the last position prior to update
            Vector2 PreviousPosition = _txPosition;
            // Sample Keyboard input and adjust position
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _txPosition.X +=5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _txPosition.X -= 5;
            }
             
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _txPosition.Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _txPosition.Y -= 5;
            }
            // Collision avoidance
            // Calculate possible future rectangle based on the updated position and the existing
            // image size

            Rectangle FutureRect = new Rectangle(_txPosition.ToPoint(), _textureRect.Size);

            if(GraphicsDevice.Viewport.Bounds.Contains(FutureRect))
            {
                _textureRect = FutureRect;
            }
            else
            {
                // Readjust position as we have not accepted the move
                // Otherwise we get a jumpy positional change when we go outside and attemp
                // to keep going. Try commenting out the line and see what happens.
                _txPosition = PreviousPosition;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture2D, _textureRect, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}