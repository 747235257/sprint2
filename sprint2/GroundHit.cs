using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class GroundHit: INPC
    {
        public INPCSprite GroundHitSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private Game game;
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private bool isAlive;

        private enum HitboxDims
        {
            WIDTH = 288, HEIGHT = 336, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public GroundHit(Texture2D texture, SpriteBatch spriteBatch, Game game, Vector2 startPos)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            GroundHitSprite = new GroundHitSprite(this.texture, this.spriteBatch);
            count = 0;
            curdir = 0;
            isAlive = true;

            


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
            GroundHitSprite = new GroundHitSprite(texture, spriteBatch);
            curdir = 0;
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

            count++;

            if (count % 16 == 0)//Interval of a diraction generator. 
            {
                curdir = rnd.Next(0, 5);


            }

            //UPDATES positions and hitboxes

            prevPos = currPos;
            Vector2 updateMove = GroundHitSprite.Update(gametime, curdir);
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
            GroundHitSprite.Draw(currPos);
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

        public Vector2 getLastPos()
        {
            return currPos;
        }
    }
}

