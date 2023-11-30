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
        private float duration;
        private Game game;
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private bool isAlive;
        private enum attackList { leftAttack = 0, rightAttack = 1, middileBeam = 2, bulletHell = 3 }
        private enum HitboxDims
        {
            WIDTH = 74*4, HEIGHT = 48*4, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public Boss1(Texture2D texture, SpriteBatch spriteBatch, Game game, Vector2 startPos)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            BossSprite1 = new BossSprite1(this.texture, this.spriteBatch);
            count = 0;
            curdir = 0;
            isAlive = true;
            duration = 0;
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
                curAttack = rnd.Next(0, 4);

            }

            //UPDATES positions and hitboxes

            prevPos = currPos;
            Vector2 updateMove = BossSprite1.Update(gametime, curdir);
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
            isAlive = false;
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
    }
}
