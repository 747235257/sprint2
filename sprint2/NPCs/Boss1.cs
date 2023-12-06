using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class Boss1: INPC
    {
        public INPCSprite BossSprite1 { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private int curAttack;
        private bool attack;
        private bool attacking;
        private float duration;
        private Game1 game;
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private bool isAlive;
        private int health;
        private enum attackList { leftAttack = 0, rightAttack = 1, middileBeam = 2 }
        private enum HitboxDims
        {
            WIDTH = 300, HEIGHT = 192, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public Boss1(Texture2D texture, SpriteBatch spriteBatch, Game1 game, Vector2 startPos)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            BossSprite1 = new BossSprite1(this.texture, this.spriteBatch);
            count = 0;
            curdir = 0;
            isAlive = true;
            duration = 0;

            //health
            health = 20;

            //gets position of the dragon
            currPos = startPos;
            prevPos = currPos;

            //hitbox allocations
            hitbox = new Rectangle((int)currPos.X + (int)HitboxDims.X_ADJ, (int)currPos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            prevHitbox = hitbox;

            //hitboxsprite
            hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
            this.game = game;
        }





        public List<IProjectile> Attack()
        {
            
            switch (curAttack)
            {
                //left attack
                case 0:
                    game.groundHit.Add(new GroundHit(game.pixel, spriteBatch, game, new Vector2(96, 96)));
                    break;
                //right attack
                case 1:
                    game.groundHit.Add(new GroundHit(game.pixel, spriteBatch, game, new Vector2(96+288, 96)));
                    break;
                //middle beam
                case 2:
                    game.groundHit.Add(new GroundHit(game.pixel, spriteBatch, game, new Vector2(96+144, 96)));
                    break;
                default:
                    break;

            }
            return null;

        }

        public void Stop()
        {
            //Reset the npc
            count = 0;
            duration= 0;
            BossSprite1 = new BossSprite1(texture, spriteBatch);
            curdir = 0;
        }

        public List<IProjectile> Execute(GameTime gametime)
        {
            List<IProjectile> projectiles = null;
            count++;
            Vector2 updateMove = BossSprite1.Update(gametime, curdir);
            if (attack)
            {

                if (duration == 0)
                {
                    //start to draw the hitbox and warning
                    //projectiles = Attack();
                    
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                }else if(duration > 1.5 && !attacking)
                {
                    //start to attack
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                    projectiles = Attack();
                    attacking = true;
                }
                else if (duration > 2.5)//The cool down duration for an attack is 2 seconds. end attack
                {
                    duration = 0;
                    attack = false;
                    attacking= false;
                    game.groundHit.Clear();
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
                curAttack = rnd.Next(0, 3);
                attack = true;

            }

            //UPDATES positions and hitboxes

            prevPos = currPos;
           
            currPos.X += updateMove.X;
            currPos.Y += updateMove.Y;
            count = count % 16;//Reset the count to prevent unnecessary storage usage.

            //UPDATES positions and hitboxes

            prevHitbox = hitbox;
            hitbox.X = (int)currPos.X + (int)HitboxDims.X_ADJ;
            hitbox.Y = (int)currPos.Y + (int)HitboxDims.Y_ADJ;

            return null;


        }
        public void Draw()
        {
            //drawHitbox();
            if (attack)
            {
                if(duration <= 1.5 && curAttack == 0)
                {
                    spriteBatch.Draw(game.pixel, new Rectangle(96, 96, 288, 336), Color.Blue);
                    spriteBatch.Draw(game.pixel, new Rectangle(96, 96, 288, (int)(336 / 1.5 * duration)), Color.Purple);
                }else if(duration <= 1.5 && curAttack == 1)
                {
                    spriteBatch.Draw(game.pixel, new Rectangle(96+288, 96, 288, 336), Color.Blue);
                    spriteBatch.Draw(game.pixel, new Rectangle(96+288, 96, 288, (int)(336 / 1.5 * duration)), Color.Purple);
                }
                else if (duration <= 1.5 && curAttack == 2)
                {
                    spriteBatch.Draw(game.pixel, new Rectangle(96+144, 96, 288, 336), Color.Blue);
                    spriteBatch.Draw(game.pixel, new Rectangle(96+144, 96, 288, (int)(336 / 1.5 * duration)), Color.Purple);
                }

            }
            spriteBatch.Draw(game.pixel, new Rectangle(190, 46, 388, 20), Color.White);
            spriteBatch.Draw(game.pixel, new Rectangle(192, 48, 384/20*health, 16), Color.Red);
            BossSprite1.Draw(currPos);
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
            health -= 1;
            if (health == 0)
            {
                isAlive = false;
                game.groundHit.Clear();
            }
            //need to set sound based on npc
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
        public Vector2 getLastPos()
        {
            return currPos;
        }
    }
}
