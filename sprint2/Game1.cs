using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Net.Mail;

namespace sprint2
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private SpriteFont bannerFont;


        private Vector2 initPosition;
        private float gameTimeElapsed; // Variable to store elapsed time


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
        public Texture2D Boss1;
        public Texture2D NPCs;
        public Texture2D ItemSprite;
        public Texture2D LevelBack;
        public Texture2D pixel;
        public Texture2D DeathScreen; //CHANGE
        public Texture2D VictoryScreen; //CHANGE
        public Texture2D Scoreboard;
        public Texture2D Banner;
        public Texture2D ranChests;

        public IPlayer player;
        private IController keyboard;


        public int blockRow = 3;
        public int blockCol = 4;
        public int chestRow = 1;
        public int chestCol = 2;

        public int pickupCount { get; set; }//counter for item pickup
        public int roomCount { get; set; } //counter for room travelled
        public int killCount {  get; set; }//counter for enemies killed

        float displayTime = 3f; // Time to display the texture in seconds
        float victoryTimer = 0f;
        bool isTextureVisible = false;

        public List<IProjectile> playerProjectiles;
        public List<IProjectile> enemyProjectiles;
        public List<IBlock> blocks;
        public List<INPC> NPCList;

        public List<IItem> items { get; set; }

        public List<INPC> groundHit;


        public List<IChest> chests;
        public const int MAX_TRANSITION = 100;
        public bool inTransition { get; set; }
        public int transitionCount { get; set; }

        private IItem item;

        public ItemCreator itemCreator { get; set; }
        public  EnemyCreator enemyCreator { get; set; }
        public CollisionHandler collision;
        public LevelManager levelManager;
        public Level curLevel;
        public ObstacleHandler obstacleHandler;
        public RandomLevelHandler randomLevelHandler;
        public List<Rectangle> wallHitboxes;
        public List<DoorHitbox> doors;
        public List<Rectangle> doorHitboxes;
        public List<LockDoorInstance> lockDoorInstances;
        private MusicManager music;
        private Inventory inventoryScreen;
        public RandomChest chest;

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
            inTransition = true;
        }

        protected override void Initialize()
        {
            gameTimeElapsed = 0f; // Initialize the timer to zero
            // TODO: Add your initialization logic here
            music = new MusicManager(this);
            levelManager = new LevelManager();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            levelManager.LoadLevels("Content/levels/level1.json");
            curLevel = levelManager.Levels[0];          
            initPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            player = new Player(this, _graphics, _spriteBatch, new Vector2(250, 250));
            collision = new CollisionHandler(this);
            itemCreator = new ItemCreator(this);
            enemyCreator = new EnemyCreator(this);
            inventoryScreen = new Inventory(this, _spriteBatch);
            //loads kb and mouse support
            keyboard = new KeyboardCont(this, player, inventoryScreen, music);
            playerProjectiles = new List<IProjectile>();
            enemyProjectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
            items = new List<IItem>();
            wallHitboxes= new List<Rectangle>();
            doorHitboxes= new List<Rectangle>();
            doors= new List<DoorHitbox>();
            NPCList = new List<INPC>();

            groundHit  = new List<INPC>();

            chests = new List<IChest>();

            lockDoorInstances = new List<LockDoorInstance>();
            timer = 0;
            keyEn = false;

            //game is not paused at the start
            /*TRUE BECAUSE TESTING INVENTORY*/
            gamePaused = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
 
            //HUD Loading
            hud = new HUD(HUDpos, this, _spriteBatch);
            hud.AddToGrid(curLevel.Name);

            Enemies = Content.Load<Texture2D>("Enemies");
            Bosses = Content.Load<Texture2D>("Bosses");
            Boss1 = Content.Load<Texture2D>("Enemy_Concept");
            NPCs = Content.Load<Texture2D>("NPCs");
            LevelBack = Content.Load<Texture2D>("levels/Level1");
            Blocks = Content.Load<Texture2D>("zeldaBlocks");
            ranChests = Content.Load<Texture2D>("Chest");
            DeathScreen = Content.Load<Texture2D>("DeathScreen"); //CHANGE
            VictoryScreen = Content.Load<Texture2D>("NewVictoryScreen"); //CHANGE
            Scoreboard = Content.Load<Texture2D>("blank");
            spriteFont = Content.Load<SpriteFont>("File");
            bannerFont = Content.Load<SpriteFont>("score");
            Banner = Content.Load<Texture2D>("banner");
            
            blocks.Add(new Block(Blocks, blockRow, blockCol, initPosition, _spriteBatch, this));
            //Create NPCs
            CreateNPCs();
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            
            obstacleHandler = new ObstacleHandler(this, this, Blocks, ranChests);
            randomLevelHandler = new RandomLevelHandler(this, blocks);

            wallHitboxes = WallHitboxHandler();
            doors = DoorHitboxHandler();
            LockDoorHandler();
            obstacleHandler.Update();
            randomLevelHandler.Update();
            //ItemSprite = Content.Load<Texture2D>("Sheet");
            //item = new Item(ItemSprite, 9, 8, new Vector2(750, 20));
            SoundManager.Instance.InitializeSound(this);

            music.InitializeMusic(this);
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!inTransition || curLevel.getClearStatus())
            {
                //If game is not over, update the time
                if (player.isAlive() && !player.getHasWon())
                {
                    // Update the timer
                    gameTimeElapsed += deltaTime;
                }


                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    this.Initialize();
                    /*Reset all the counts for scoreboard to 0*/
                    pickupCount = 0;
                    roomCount = 0;
                    killCount = 0;
                }

            inventoryScreen.updateInventory();
            inventoryScreen.updateCounterInventory();
            updatePauseCounter();
            keyboard.HandlePause();
                keyboard.HandleMuteMusic();
            keyboard.RegisterCommand();
            if(gamePaused)keyboard.HandleSwitchInventory();
            
            if (!gamePaused)
            {
                keyboard.handleLevelSwitch();
                keyboard.HandleLevelDebug();
                keyboard.HandleMovement();
                keyboard.HandleAttack();
                player.updatePlayer();
                curLevel.checkLevelClear(this);

                    removePlayerProjectileList();
                    removeEnemyList();
                    //projectile return by keyboard is added to the list


                List<IProjectile> plProj = keyboard.HandlePlayerItem();

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
                    collision.HandlePlayerEnemyCollision(player, groundHit);
                    //collision.HandleEnemyEnemyCollision(NPCList);
                    collision.HandleEnemyBlockCollision(NPCList, blocks);
                    collision.HandleEnemyProjectileCollision(NPCList, playerProjectiles);
                    collision.HandleEnemyWallCollision(NPCList, wallHitboxes);
                    collision.HandlePlayerWallCollision(player, wallHitboxes);
                    collision.HandleProjectileWallCollision(wallHitboxes, enemyProjectiles, playerProjectiles);
                    collision.HandlePlayerChestCollision(player, chests, items, _spriteBatch);
                    collision.HandleEnemyChestCollision(NPCList, chests);
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
            else
            {
                transitionCount++;

                if(transitionCount > MAX_TRANSITION)
                {
                    transitionCount = 0;
                    inTransition = false;
                }
            }
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
                drawAllItems();
                drawAllEnemies();
                drawAllChests();
                player.Draw();
                hud.Draw();
                if (gamePaused)
                {
                    inventoryScreen.Draw();
                }
            }
            else if (player.getHasWon())
            {
                //draw scoreboard
                drawScoreboard();
                //draw win message
                drawWin();
                
                
                
            }
            else if (!player.isAlive())
            {
                //drawscoreboard
                drawScoreboard();
                //draw lose wo
                drawLose();
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
                if (projectile != null)
                {
                    projectile.UpdatePosition(gameTime);
                }
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
        private void drawAllChests()
        {
            foreach (IChest chest in chests)
            {
                if (chest != null) chest.drawRandomChest();
            }
        }

        private void drawAllEnemies()
        {
            foreach (INPC enemy in groundHit)
            {
                if (enemy != null) enemy.Draw();
            }
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
            _spriteBatch.DrawString(bannerFont, "YOU WIN! ", new Vector2(450,140 ), Color.Green);
        }
        private void drawLose()
        {
            _spriteBatch.DrawString(bannerFont, "YOU LOSE! ", new Vector2(450, 140), Color.Red);
        }
        private void drawScoreboard()
        {
            string time = gameTimeElapsed.ToString("0.00") ;
           
            _spriteBatch.Draw(Scoreboard, new Rectangle(0, 0, 768, 528), Color.Blue);
            _spriteBatch.DrawString(spriteFont, "Enemy Killed : " + killCount + "\n\nTime Played : " + time  + "\n\nItems Collected : " + pickupCount + "\n\nRoom Traveled : " + roomCount , new Vector2(30, 200), Color.White);
            _spriteBatch.DrawString(bannerFont, "SCOREBOARD ", new Vector2(170, 40), Color.White);
            _spriteBatch.Draw(Banner, new Vector2(35, -100), Color.White);
        }
    }
}