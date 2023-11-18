using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;

namespace sprint2
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;


        private Vector2 initPosition;


        private INPC Dragon;
        private INPC Skull;
        private INPC OldMan;
        private INPC Goriya;
        private INPC Gel;
        private INPC Bat;
        public INPC cur;
        public int currentNPC { get; set; }


        public Texture2D Blocks;

        private float timer;
        private bool keyEn;

        public Texture2D Enemies;
        public Texture2D Bosses;
        public Texture2D NPCs;
        public Texture2D ItemSprite;
        public Texture2D LevelBack;
        public Texture2D pixel;
        public Texture2D DeathScreen;
        public Texture2D VictoryScreen;

        public IPlayer player;
        private IController keyboard;


        public int blockRow = 3;
        public int blockCol = 4;


        public List<IProjectile> playerProjectiles;
        public List<IProjectile> enemyProjectiles;
        public List<IBlock> blocks;
        public List<INPC> NPCList;
        public List<IItem> items;


        private IItem item;

        private CollisionHandler collision;
        public LevelManager levelManager;
        public Level curLevel;
        public ObstacleHandler obstacleHandler;
        public List<Rectangle> wallHitboxes;
        public List<DoorHitbox> doors;
        public List<Rectangle> doorHitboxes;
        public List<LockDoorInstance> lockDoorInstances;
        private MusicManager music;
        private Inventory inventoryScreen;

        //HUD RELATED CONSTANTS
        public HUD hud;
        private Vector2 HUDpos = new Vector2(0, 528);
        private int HUDHeight = 150;

        //GAME STATE variables
        public bool gamePaused;
        private int pauseCounter;
        private const int MAX_PAUSE = 10;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //window size
            _graphics.PreferredBackBufferHeight = 528 + HUDHeight; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            _graphics.PreferredBackBufferWidth = 768; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
    }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            levelManager = new LevelManager();
            
            levelManager.LoadLevels("Content/levels/level1.json");
            curLevel = levelManager.Levels[0];
            

            initPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            
            collision = new CollisionHandler();

            //loads kb and mouse support
            keyboard = new KeyboardCont(this);
            playerProjectiles = new List<IProjectile>();
            enemyProjectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
            items = new List<IItem>();
            wallHitboxes= new List<Rectangle>();
            doorHitboxes= new List<Rectangle>();
            doors= new List<DoorHitbox>();
            NPCList = new List<INPC>();
            lockDoorInstances = new List<LockDoorInstance>();
            music = new MusicManager(this);
            //loads kb and mouse support
            timer = 0;
            keyEn = false;

            //game is not paused at the start
            /*TRUE BECAUSE TESTING INVENTORY*/
            gamePaused = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            player = new Player(this, _graphics, _spriteBatch, new Vector2 (250, 250));
            inventoryScreen = new Inventory(this, _spriteBatch);
            //HUD Loading
            hud = new HUD(HUDpos, this, _spriteBatch);
            hud.AddToGrid(curLevel.Name);

            Enemies = Content.Load<Texture2D>("Enemies");
            Bosses = Content.Load<Texture2D>("Bosses");
            NPCs = Content.Load<Texture2D>("NPCs");
            LevelBack = Content.Load<Texture2D>("levels/Level1");
            Blocks = Content.Load<Texture2D>("zeldaBlocks");
            DeathScreen = Content.Load<Texture2D>("DeathScreen");
            VictoryScreen = Content.Load<Texture2D>("NewVictoryScreen");
            blocks.Add(new Block(Blocks, blockRow, blockCol, initPosition, _spriteBatch, this));
            //Create NPCs
            CreateNPCs();
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            obstacleHandler = new ObstacleHandler(this, this, Blocks);
            wallHitboxes = WallHitboxHandler();
            doors = DoorHitboxHandler();
            LockDoorHandler();
            obstacleHandler.Update();
            //ItemSprite = Content.Load<Texture2D>("Sheet");
            //item = new Item(ItemSprite, 9, 8, new Vector2(750, 20));
            SoundManager.Instance.InitializeSound(this);

            music.InitializeMusic(this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                this.Initialize();
            }

            inventoryScreen.updateInventory();
            inventoryScreen.updateCounterInventory();
            updatePauseCounter();
            keyboard.HandlePause(this);
            if(gamePaused)keyboard.HandleSwitchInventory(player, inventoryScreen);
            
            if (!gamePaused)
            {
                keyboard.handleLevelSwitch(this);
                keyboard.HandleMovement(_graphics, player);
                Vector2 range = keyboard.HandleAttack(_graphics, player);
                keyboard.HandleDamaged(_graphics, player);

                player.updatePlayer();
                curLevel.checkLevelClear(this);

                removePlayerProjectileList();
                removeEnemyList();
                //projectile return by keyboard is added to the list


                List<IProjectile> plProj = keyboard.HandlePlayerItem(_graphics, player);

                if (plProj != null) playerProjectiles.AddRange(plProj);


                UpdatePlayerProjectileList(gameTime);


                removeEnemyProjectileList();
                updateEnemyProjectileList(gameTime);
                updateEnemyList(gameTime);

                collision.HandlePlayerParryProjectile(player, enemyProjectiles, this);
                collision.HandlePlayerAttackCollision(player, NPCList);
                collision.HandlePlayerProjectileCollision(player, enemyProjectiles);
                collision.HandleProjectileBlockCollision(blocks, enemyProjectiles, playerProjectiles);
                collision.HandlePlayerBlockCollision(player, blocks);
                collision.HandlePlayerEnemyCollision(player, NPCList);
                collision.HandleEnemyEnemyCollision(NPCList);
                collision.HandleEnemyBlockCollision(NPCList, blocks);
                collision.HandleEnemyProjectileCollision(NPCList, playerProjectiles);
                collision.HandleEnemyWallCollision(NPCList, wallHitboxes);
                collision.HandlePlayerWallCollision(player, wallHitboxes);
                collision.HandleProjectileWallCollision(wallHitboxes, enemyProjectiles, playerProjectiles);
                collision.HandlePlayerDoorCollision(player, doorHitboxes, doors, this);
                //collision.HandleEnemyEnemyProjectileCollision(NPCList, enemyProjectiles);
                collision.HandleEnemyDoorCollision(NPCList, doorHitboxes);
                collision.HandleProjectileDoorCollision(doorHitboxes, enemyProjectiles, playerProjectiles);
                collision.HandlePlayerItemCollision(items, player);
                collision.HandleEnemyLockDoorCollision(NPCList, lockDoorInstances);
                collision.HandlePlayerLockDoorCollision(player, lockDoorInstances, this);
                collision.HandleProjectileLockDoorCollision(lockDoorInstances, enemyProjectiles, playerProjectiles);
                if (player.getHasWon() || !player.isAlive())
                {
                    pauseGame();
                }
            }
         //if (!player.isAlive()) this.Initialize();
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            //TUTORIAL
            _spriteBatch.Begin();
            //256, 176
   
            //for(int i = 0; i < curLevel.WallHitboxs.Count; i++)
            //{
            //    _spriteBatch.Draw(pixel, new Rectangle((int)curLevel.WallHitboxs[i].X, (int)curLevel.WallHitboxs[i].Y, curLevel.WallHitboxs[i].Width, curLevel.WallHitboxs[i].Height), Color.Blue);
            //}
            if (player.isAlive() && !player.getHasWon())
            {
                _spriteBatch.Draw(LevelBack, new Rectangle(0, 0, 768, 528), Color.White);
                drawAllBlocks();
                drawAllProjectiles();
                drawAllEnemies();
                drawAllItems();
                player.Draw();
                hud.Draw();
                if (gamePaused)
                {
                    inventoryScreen.Draw();
                }
            }
            else if (player.getHasWon())
            {
                drawWin();
            }
            else if (!player.isAlive())
            {
                drawDeath();
            }
            _spriteBatch.End();

            //item.ItemProcess(_spriteBatch);

            base.Draw(gameTime);
        }

        public void pauseGame()
        {
            if(pauseCounter >= MAX_PAUSE)
            {
                SoundEffectInstance pauseGame = SoundManager.Instance.CreateSound("pause");
                pauseGame.Play();
                gamePaused = !gamePaused;
                pauseCounter = 0;
                inventoryScreen.resetItemIndex();
            }
        }

        public void updatePauseCounter()
        {
            pauseCounter++;
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

        private void drawAllItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null && items[i].isAlive())
                {
                    items[i].Draw();
                }
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
            //Skull = new Skull(Enemies, _spriteBatch, this);
            //OldMan = new OldMan(NPCs, _spriteBatch, this);
            //Goriya = new Goriya(Enemies, _spriteBatch, this);

            //Gel = new Gel(Enemies, _spriteBatch, this);
            //Bat = new Bat(Enemies, _spriteBatch, this);
            //Dragon = new Dragon(Bosses, _spriteBatch, this);

            //NPCList.Add(Goriya);
            //NPCList.Add(Dragon);
            //NPCList.Add(Bat);
            //NPCList.Add(Skull);

        }

        public List<Rectangle> WallHitboxHandler()
        {
            List<Rectangle> list = new List<Rectangle>();
            for(int i = 0; i < curLevel.WallHitboxs.Count; i++)
            {
                list.Add(new Rectangle((int)curLevel.WallHitboxs[i].X, (int)curLevel.WallHitboxs[i].Y, curLevel.WallHitboxs[i].Width, curLevel.WallHitboxs[i].Height));
            }
            return list;
        }


        public List<DoorHitbox> DoorHitboxHandler()
        {
            List<Rectangle> list = new List<Rectangle>();
            for (int i = 0; i < curLevel.DoorHitboxs.Count; i++)
            {
                list.Add(new Rectangle((int)curLevel.DoorHitboxs[i].X, (int)curLevel.DoorHitboxs[i].Y, curLevel.DoorHitboxs[i].Width, curLevel.DoorHitboxs[i].Height));
            }
            doorHitboxes = list;
            return curLevel.DoorHitboxs;
        }

        public void LockDoorHandler()
        {
            List<LockDoorInstance> list = new List<LockDoorInstance>();
            for (int i = 0; i < curLevel.LockDoors.Count; i++)
            {
                list.Add(new LockDoorInstance(this, curLevel.LockDoors[i]));
            }
            lockDoorInstances = list;
            
        }

        private void drawDeath()
        {
            _spriteBatch.Draw(DeathScreen, new Rectangle(0, 0, 768, 528), Color.White);
        }
        private void drawWin()
        {
            _spriteBatch.Draw(VictoryScreen, new Rectangle(0, 0, 768, 700), Color.White);
        }
    }
}