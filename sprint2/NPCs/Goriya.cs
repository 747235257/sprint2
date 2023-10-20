using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace sprint2
{
    public class Goriya:INPC
    {
        public INPCSprite GoriyaSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private int attackDir;
        private float duration;
        private bool attack;
        private Rectangle destination;
        private Game1 game;
        private string Name;
        private ProjectileFactory factory = new ProjectileCreator();
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private bool isAlive;

        private enum HitboxDims
        {
            WIDTH = 32, HEIGHT = 32, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public Goriya(Texture2D texture, SpriteBatch spriteBatch, Game1 game, Vector2 startPos)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.Name = "Goriya";
            GoriyaSprite = new GoriyaSprite(this.texture, this.spriteBatch);
            count = 0;
            duration = 0;
            attack = false;
            this.game = game;
            isAlive = true;

            //gets position of the dragon
            currPos = startPos;
            prevPos = currPos;

            //hitbox allocations
            hitbox = new Rectangle((int)currPos.X + (int)HitboxDims.X_ADJ, (int)currPos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            prevHitbox = hitbox;

            //hitboxsprite
            hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
        }





        public List<IProjectile> Attack()
        {
            List<IProjectile> projectiles = new List<IProjectile>();
            Vector2 dir = new Vector2();
            switch (attackDir)
            {
                case 1:
                    dir = new Vector2(0,-1);
                    break;
                case 2:
                    dir = new Vector2(0,1);
                    break ;
                case 3:
                    dir = new Vector2(-1,0);
                    break;
                case 4:
                    dir = new Vector2(1,0);
                    break;
                default:
                    break;

            }
            
            projectiles.Add(factory.GetProjectile(Name, new Vector2(currPos.X, currPos.Y), game.Content, dir));
            return projectiles;

        }
        public void Stop()
        {
            //Reset the npc.
            count = 0;
            duration = 0;
            GoriyaSprite = new GoriyaSprite(texture, spriteBatch);
        }

        public List<IProjectile> Execute(GameTime gametime)
        {
            List<IProjectile> projectiles = null;
            count++;
            if (attack)
            {
                
                if (duration == 0)
                {
                    projectiles = Attack();
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                }
                else if (duration > 2)//The cool down duration for an attack is 2 seconds.
                {
                    duration = 0;
                    attack = false;
                }
                else
                {
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                    return projectiles;
                }
            }
            if (count % 16 == 0)//Interval of a diraction generator.
            {
                curdir = rnd.Next(0, 5);
                if (curdir == 0)
                {
                    
                    attack = true;
                }
                else
                {
                    attackDir = curdir;
                }

            }
 
            prevPos = currPos;
            Vector2 updateMove = GoriyaSprite.Update(gametime, curdir);
            count = count % 16;//Reset the count to prevent unnecessary storage usage.
            currPos.X += updateMove.X;
            currPos.Y += updateMove.Y;

            //UPDATES positions and hitboxes

            prevHitbox = hitbox;
            hitbox.X = (int)currPos.X + (int)HitboxDims.X_ADJ;
            hitbox.Y = (int)currPos.Y + (int)HitboxDims.Y_ADJ;
            return projectiles;


        }
        public void Draw()
        {
            drawHitbox();
            GoriyaSprite.Draw(currPos);
        }

        //COLLISION SPRINT3
        public void drawHitbox()
        {
            hitboxSprite.DrawHitbox(spriteBatch, new Vector2(hitbox.X, hitbox.Y), hitbox);
        }

        //COLLISION SPRINT3
        public Rectangle getHitbox()
        {
            return hitbox;
        }

        public void giveDamage()
        {
            isAlive = false;
        }

        public bool isStillAlive()
        {
            return isAlive;
        }

        public void setLastPos()
        {
            currPos = prevPos;
            hitbox = prevHitbox;
        }
    }
}
