using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

using System.Collections.Generic;
using System.ComponentModel;


namespace sprint2
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Vector2 initPosition;


        private INPC Dragon;
        private INPC Skull;
        private INPC OldMan;
        private INPC Goriya;
        private INPC Gel;
        private INPC Bat;
        public INPC cur;
        public int currentNPC { get; set; }


        Texture2D Blocks;

        private float timer;
        private bool keyEn;

        Texture2D Enemies;
        Texture2D Bosses;
        Texture2D NPCs;
        Texture2D ItemSprite;

        private IPlayer player;
        private IController keyboard;


        private int blockRow = 3;
        private int blockCol = 4;


        private List<IProjectile> playerProjectiles;
        private List<IProjectile> enemyProjectiles;
        private List<IBlock> blocks;
        private List<INPC> NPCList;


        private IItem item;

        private CollisionHandler collision;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            initPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            



            collision = new CollisionHandler();

            //loads kb and mouse support
            keyboard = new KeyboardCont(this);
            playerProjectiles = new List<IProjectile>();
            enemyProjectiles = new List<IProjectile>();
            blocks = new List<IBlock>();

            // TODO: Add your initialization logic here
            NPCList = new List<INPC>();
            //loads kb and mouse support
            timer = 0;
            keyEn = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            player = new Player(this, _graphics, _spriteBatch);

            Enemies = Content.Load<Texture2D>("Enemies");
            Bosses = Content.Load<Texture2D>("Bosses");
            NPCs = Content.Load<Texture2D>("NPCs");

            Blocks = Content.Load<Texture2D>("zeldaBlocks");
            blocks.Add(new Block(Blocks, blockRow, blockCol, initPosition, _spriteBatch, this));
            //Create NPCs
            CreateNPCs();
       

         
            ItemSprite = Content.Load<Texture2D>("Sheet");
            item = new Item(ItemSprite, 9, 8, new Vector2(750, 20));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                this.Initialize();
            }


            keyboard.HandleItem(_graphics, item);
            keyboard.HandleMovement(_graphics, player);
            Vector2 range = keyboard.HandleAttack(_graphics, player);
            keyboard.HandleDamaged(_graphics, player);

            player.updatePlayer();

            removePlayerProjectileList();
            removeEnemyList();
            //projectile return by keyboard is added to the list
            IProjectile plProj = keyboard.HandlePlayerItem(_graphics, player);

            if(plProj != null) playerProjectiles.Add(plProj);

            UpdatePlayerProjectileList(gameTime);


            //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if(timer > 0.5 && !keyEn)
            //{
            //    keyEn = keyboard.HandleSwitchEnemy(currentNPC);
            //    if(keyEn)
            //    {
            //        timer = 0;
            //    }
            //}else if(timer > 0.5 && keyEn)
            //{
            //    keyEn = false;
            //}
            
            

            removeEnemyProjectileList();
            updateEnemyProjectileList(gameTime);
            updateEnemyList(gameTime);

            collision.HandlePlayerProjectileCollision(player, enemyProjectiles);
            collision.HandleProjectileBlockCollision(blocks, enemyProjectiles, playerProjectiles);
            collision.HandlePlayerBlockCollision(player, blocks);
            collision.HandlePlayerEnemyCollision(player, NPCList);
            collision.HandleEnemyEnemyCollision(NPCList);
            collision.HandleEnemyBlockCollision(NPCList, blocks);
            collision.HandleEnemyProjectileCollision(NPCList, playerProjectiles);
            collision.HandleEnemyEnemyProjectileCollision(NPCList, enemyProjectiles);


            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);

            // TODO: Add your drawing code here

            //TUTORIAL
            _spriteBatch.Begin();

            drawAllBlocks();
            drawAllProjectiles();
            drawAllEnemies();
            player.Draw();

            _spriteBatch.End();

            item.ItemProcess(_spriteBatch);

            base.Draw(gameTime);
        }



        private void updateEnemyList(GameTime gameTime)
        {
            foreach(INPC enemy in NPCList)
            {
                if(enemy != null) 
                {
                    List<IProjectile> proj = enemy.Execute(gameTime);

                    if (proj != null) enemyProjectiles.AddRange(proj);
                }
            }
        }

        private void removeEnemyList()
        {

            for (int i = 0; i < NPCList.Count; i++)
            {
                if (NPCList[i] != null && !NPCList[i].isStillAlive()) NPCList[i] = null;
            }

        }
        private void removePlayerProjectileList()
        {
            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                if (playerProjectiles[i] != null && !playerProjectiles[i].ReturnStatus()) playerProjectiles[i] = null;
            }
        }

        private void UpdatePlayerProjectileList(GameTime gameTime)
        {
            foreach (IProjectile projectile in playerProjectiles)
            {
                if (projectile != null) projectile.UpdatePosition(gameTime);
            }
        }
        private void drawAllProjectiles()
        {
            foreach (IProjectile projectile in enemyProjectiles)
            {
                if(projectile != null) projectile.Draw(_spriteBatch);
            }

            foreach (IProjectile projectile in playerProjectiles)
            {
                if(projectile != null) projectile.Draw(_spriteBatch);
            }
        }

        private void drawAllBlocks()
        {
            foreach (IBlock block in blocks)
            {
                if (block != null) block.drawBlock();
            }
        }

        private void drawAllEnemies()
        {
            foreach (INPC enemy in NPCList)
            {
                if (enemy != null) enemy.Draw();
            }
        }

        private void updateEnemyProjectileList(GameTime gameTime)
        {
            foreach (IProjectile projectile in enemyProjectiles)
            {
                if(projectile != null) projectile.UpdatePosition(gameTime);
                
            }
        }

        private void removeEnemyProjectileList()
        {
            for(int i = 0; i < enemyProjectiles.Count; i++)
            {
                if (enemyProjectiles[i] != null && !enemyProjectiles[i].ReturnStatus()) enemyProjectiles[i] = null;
            }
        }


        private void CreateNPCs()
        {
            Skull = new Skull(Enemies, _spriteBatch, this);
            //OldMan = new OldMan(NPCs, _spriteBatch, this);
            Goriya = new Goriya(Enemies, _spriteBatch, this);

            //Gel = new Gel(Enemies, _spriteBatch, this);
            Bat = new Bat(Enemies, _spriteBatch, this);
            Dragon = new Dragon(Bosses, _spriteBatch, this);

            NPCList.Add(Goriya);
            NPCList.Add(Dragon);
            NPCList.Add(Bat);
            NPCList.Add(Skull);

        }
    }
}