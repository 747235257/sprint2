using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace sprint2
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private IPlayer player;
        private IController keyboard;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(new Vector2(50, 50));

            Texture2D TRightWalk = Content.Load<Texture2D>("RightWalk");
            Texture2D TLeftWalk = Content.Load<Texture2D>("LeftWalk");
            Texture2D TUpWalk = Content.Load<Texture2D>("UpWalk");
            Texture2D TDownWalk = Content.Load<Texture2D>("DownWalk");
            Texture2D TInitialStand = Content.Load<Texture2D>("InitialStand");
            player.LoadSprite(TRightWalk, TLeftWalk, TUpWalk, TDownWalk, TInitialStand);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Texture2D TRightWalk = Content.Load<Texture2D>("RightWalk");
            //Texture2D TLeftWalk = Content.Load<Texture2D>("LeftWalk");
            //Texture2D TUpWalk = Content.Load<Texture2D>("UpWalk");
            //Texture2D TDownWalk = Content.Load<Texture2D>("DownWalk");
            //Texture2D TInitialStand = Content.Load<Texture2D>("InitialStand");
            //player.LoadSprite(TRightWalk, TLeftWalk, TUpWalk, TDownWalk, TInitialStand);

            //loads kb and mouse support
            keyboard = new KeyboardCont();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton != 0)
                Exit();

            keyboard.Handle(_graphics, player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);

            // TODO: Add your drawing code here

            //TUTORIAL
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}