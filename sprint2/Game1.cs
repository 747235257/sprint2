using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.ComponentModel;

namespace sprint2
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        private INPC Dragon;
        private INPC Skull;
        private INPC OldMan;
        private INPC Goriya;
        private INPC Gel;
        private INPC Bat;
        private INPC cur;
        private ArrayList NPCList;
        public int currentNPC { get; set; }

        Texture2D Enemies;
        Texture2D Bosses;
        Texture2D NPCs;

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
            NPCList= new ArrayList();
            //loads kb and mouse support
            keyboard = new KeyboardCont(this, currentNPC);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            Texture2D TRightWalk = Content.Load<Texture2D>("RightWalk");
            Texture2D TLeftWalk = Content.Load<Texture2D>("LeftWalk");
            Texture2D TUpWalk = Content.Load<Texture2D>("UpWalk");
            Texture2D TDownWalk = Content.Load<Texture2D>("DownWalk");
            Texture2D TInitialStand = Content.Load<Texture2D>("InitialStand");
            Enemies = Content.Load<Texture2D>("Enemies");
            Bosses = Content.Load<Texture2D>("Bosses");
            NPCs = Content.Load<Texture2D>("NPCs");

            //Create NPCs
            
            CreateNPCs();
            
            
            player.LoadSprite(TRightWalk, TLeftWalk, TUpWalk, TDownWalk, TInitialStand);

            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton != 0)
                Exit();
            
            keyboard.Handle(_graphics, player);
            
            cur = (INPC)NPCList[currentNPC];
            cur.Execute(gameTime);
            Dragon.Execute(gameTime);

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

            
            cur.Draw();
            base.Draw(gameTime);
        }
        
        private void CreateNPCs()
        {
            Skull = new Skull(Enemies, _spriteBatch);
            OldMan = new OldMan(NPCs, _spriteBatch); 
            Goriya = new Goriya(Enemies, _spriteBatch);
            Gel = new Gel(Enemies, _spriteBatch);
            Bat = new Bat(Enemies, _spriteBatch);
            Dragon = new Dragon(Bosses, _spriteBatch);

            NPCList.Add(Skull);
            NPCList.Add(OldMan);
            NPCList.Add(Goriya);
            NPCList.Add(Gel);
            NPCList.Add(Bat);
            NPCList.Add(Dragon);
            
        }
    }
}