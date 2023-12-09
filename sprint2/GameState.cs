using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace sprint2
{
    internal class GameState
    {
        /*
            * The Game would start at the start screen. Press any key then 
            * post change state from START_SCREEN to GAMEPLAY
            * 
            * In GAMEPLAY STATE: 
            * State Criteria:
            * Draw Gameplay
            * Enable all gameplay activity and keybind
            * Pwease Write More Nyagwestion UwU >.<
            * 
            * State Change Condition:
            * If paused key pressed, change state to PAUSE state
            * If player hp == 0, change to DEATH state
            * If objective secured, change to VICTORY state
            * 
            * In PAUSE STATE:
            * State Criteria:
            * Draw Inventory
            * Stop All Gameplay Activity (Damage taken, movement keybind etc)
            * 
            * State Change Condition:
            * If Unpaused key pressed, change state to GAMEPLAY state
            * If Exit gameplay key pressed, change state to START_SCREEN
            * 
            * In DEATH STATE:
            * Draw DeathScreen
            * Suggestion: Play Death Music
            * Stop All Gameplay Activity (Optional: Pause the activity, maybe a continue button)
            * 
            * State Change Condition:
            * If Reset key pressed, change state to GAMEPLAY state
            * If Exit gameplay key pressed, change state to START_SCREEN
            * 
            * In VICTORY STATE:
            * Draw VictoryScreen
            * Suggestion: Play Victory Music
            * Stop All Gameplay Activity (Optional: Pause the activity, maybe a continue button)
            * 
            * State Change Condition:
            * If Reset key pressed, change state to GAMEPLAY state
            * If Next level key pressed, go to next level in GAMEPLAY state
            * If Exit gameplay key pressed, change state to START_SCREEN
            */
            

        private enum State
        {
            START_SCREEN, GAMEPLAY, PAUSE, DEATH, VICTORY
        }

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public List<IProjectile> playerProjectiles;
        public List<IProjectile> enemyProjectiles;
        public List<IBlock> blocks;
        public List<INPC> NPCList;
        public List<IItem> items;

        public IPlayer player;
        private IController keyboard;

        //HUD RELATED CONSTANTS
        public HUD hud;
        private Vector2 HUDpos = new Vector2(0, 528);
        private int HUDHeight = 150;

        internal Texture2D LevelBack;
        internal Texture2D StartScreenSprite;

        private CollisionHandler collision;
        public LevelManager levelManager;
        public Level curLevel;
        public ObstacleHandler obstacleHandler;
        public List<Rectangle> wallHitboxes;
        public List<DoorHitbox> doors;
        public List<Rectangle> doorHitboxes;
        private MusicManager music;

        public GameTime _gameTime;
        public Game1 _game;

        State _state;

        public GameState(Game1 game, GameTime gameTime)
        {
            _gameTime = gameTime;
            _game = game;
        }

        public void LoadContent()
        {
            LevelBack = _game.Content.Load<Texture2D>("levels/Level1");
            StartScreenSprite = _game.Content.Load<Texture2D>("StartScreePrototype");
        }

        public void drawGame()
        {
            _spriteBatch.Draw(LevelBack, new Rectangle(0, 0, 768, 528), Color.White);
            drawAllBlocks();
            drawAllProjectiles();
            drawAllEnemies();
            drawAllItems();
            player.Draw();
            hud.Draw();
        }
        public void GameStateMachine()
        {

        }
        public void GameStateMachine (Game game)
        {
            _state = State.START_SCREEN;
            if (_state == State.START_SCREEN) 
            {
                _spriteBatch.Draw(StartScreenSprite, new Rectangle(0, 0, 768, 528), Color.White);
                //drawStartScreen
            }
            else if (_state == State.GAMEPLAY)
            {
                drawGame();
            }
            else if(_state == State.PAUSE) 
            {
                //_spriteBatch.Draw(Inventory, new Rectangle(0, 0, 768, 528), Color.White);
            }
            else if(_state == State.DEATH) 
            {
                //_spriteBatch.Draw(DeathScreen, new Rectangle(0, 0, 768, 528), Color.White);
            }
            else if (_state == State.VICTORY) 
            {
                //_spriteBatch.Draw(VictoryScreen, new Rectangle(0, 0, 768, 528), Color.White);
            }

        }

        public void stateUpdater()
        {
            if (_state == State.START_SCREEN)
            {
                //Keyboard Bind, Any Key -> Change State to gameplay
            }
            else if (_state == State.GAMEPLAY)
            {
                updateGameplay(_game, _gameTime);

                if (!player.isAlive())
                {
                    _state = State.DEATH;
                }
                if (!player.getHasWon())
                {
                    _state = State.VICTORY;
                }
            }
            else if (_state == State.PAUSE)
            {
                //Unpause -> Go Back To Gameplay, Quit -> Go Back to Start Screen
            }
            else
            {
                //Restart -> Go Back To Gameplay, Quit -> Go Back to Start Screen
            }
        }
        private void updateGameplay(Game1 game, GameTime gameTime) 
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                game.Exit();

            keyboard.handleLevelSwitch();
            keyboard.HandleMovement();
            keyboard.HandleAttack();
            player.updatePlayer();

            removePlayerProjectileList();
            removeEnemyList();
            //projectile return by keyboard is added to the list
            List<IProjectile> plProj = keyboard.HandlePlayerItem();

            if (plProj != null) playerProjectiles.AddRange(plProj);

            UpdatePlayerProjectileList(gameTime);
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
            collision.HandleEnemyWallCollision(NPCList, wallHitboxes);
            collision.HandlePlayerWallCollision(player, wallHitboxes);
            collision.HandleProjectileWallCollision(wallHitboxes, enemyProjectiles, playerProjectiles);
            collision.HandlePlayerDoorCollision(player, doorHitboxes, doors, game);
            //collision.HandleEnemyEnemyProjectileCollision(NPCList, enemyProjectiles);
            collision.HandleEnemyDoorCollision(NPCList, doorHitboxes);
            collision.HandleProjectileDoorCollision(doorHitboxes, enemyProjectiles, playerProjectiles);
            collision.HandlePlayerItemCollision(items, player);
        }

        private void updateEnemyList(GameTime gameTime)
        {
            foreach (INPC enemy in NPCList)
            {
                if (enemy != null)
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
        private void updateEnemyProjectileList(GameTime gameTime)
        {
            foreach (IProjectile projectile in enemyProjectiles)
            {
                if (projectile != null) projectile.UpdatePosition(gameTime);

            }
        }

        private void removeEnemyProjectileList()
        {
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {
                if (enemyProjectiles[i] != null && !enemyProjectiles[i].ReturnStatus()) enemyProjectiles[i] = null;
            }
        }

        //Function to help drawing the game
        private void drawAllProjectiles()
        {
            foreach (IProjectile projectile in enemyProjectiles)
            {
                if (projectile != null) projectile.Draw(_spriteBatch);
            }

            foreach (IProjectile projectile in playerProjectiles)
            {
                if (projectile != null) projectile.Draw(_spriteBatch);
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
    }
}
