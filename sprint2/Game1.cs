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

        private List<IProjectile> playerProjectiles;
        private List<IProjectile> enemyProjectiles;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            NPCList= new ArrayList();
            //loads kb and mouse support
            keyboard = new KeyboardCont(this);
            playerProjectiles = new List<IProjectile>();
            enemyProjectiles = new List<IProjectile>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(this, _graphics, _spriteBatch);
            Enemies = Content.Load<Texture2D>("Enemies");
            Bosses = Content.Load<Texture2D>("Bosses");
            NPCs = Content.Load<Texture2D>("NPCs");

            //Create NPCs
            
            CreateNPCs();
            
  

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton != 0)
                Exit();

            keyboard.HandleMovement(_graphics, player);
            Vector2 range = keyboard.HandleAttack(_graphics, player);
            keyboard.HandleDamaged(_graphics, player);

            player.updateAttack();
            player.updateItem();

            removePlayerProjectileList();
            //projectile return by keyboard is added to the list
            IProjectile plProj = keyboard.HandlePlayerItem(_graphics, player);

            if(plProj != null) playerProjectiles.Add(plProj);

            UpdatePlayerProjectileList(gameTime);


            keyboard.HandleSwitchEnemy(currentNPC);

            removeEnemyProjectileList();
            cur = (INPC)NPCList[currentNPC];

            List<IProjectile> projectiles = cur.Execute(gameTime);

            addEnemyProjectileList(projectiles);
            updateEnemyProjectileList(gameTime);


            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);

            // TODO: Add your drawing code here

            //TUTORIAL
            _spriteBatch.Begin();
            player.Draw();
            drawAllProjectiles();
            _spriteBatch.End();

            
            cur.Draw();
            base.Draw(gameTime);
        }

        //addEnemyProjectileList();
        //updateEnemyProjectileList();
        //removeEnemyProjectileList();

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
        private void addEnemyProjectileList(List<IProjectile> projectiles)
        {
            if(projectiles != null)
            {
                enemyProjectiles.AddRange(projectiles);
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
            Skull = new Skull(Enemies, _spriteBatch);
            OldMan = new OldMan(NPCs, _spriteBatch); 
            Goriya = new Goriya(Enemies, _spriteBatch, this);
            Gel = new Gel(Enemies, _spriteBatch);
            Bat = new Bat(Enemies, _spriteBatch);
            Dragon = new Dragon(Bosses, _spriteBatch, this);

            NPCList.Add(Skull);
            NPCList.Add(OldMan);
            NPCList.Add(Goriya);
            NPCList.Add(Gel);
            NPCList.Add(Bat);
            NPCList.Add(Dragon);
            
        }
    }
}