using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace sprint2
{
    internal class GameState
    {
        public GameState() 
        {
            
        }

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

        State _state;

        public void loadContent(Game game)
        {
            LevelBack = game.Content.Load<Texture2D>("levels/Level1");
            StartScreenSprite = game.Content.Load<Texture2D>("StartScreePrototype");
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
            else if(_state == State.GAMEPLAY) { }
            

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
