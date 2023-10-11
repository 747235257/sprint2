using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class Gel: INPC
    {
        public INPCSprite GelSprite { get; set; }
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
            WIDTH = 15, HEIGHT = 22, X_ADJ = 0, Y_ADJ = 7, ROW = 1, COL = 1
        }
        public Gel(Texture2D texture, SpriteBatch spriteBatch, Game game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            GelSprite = new GelSprite(this.texture, this.spriteBatch);
            count = 0;
            isAlive = true;

            //gets position of the dragon
            currPos = GelSprite.GetPos();
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
            GelSprite = new GelSprite(texture, spriteBatch);
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

            count++;
            
            if (count % 16 == 0)//Interval of a diraction generator.
            {
                curdir = rnd.Next(0, 5);
                

            }

            GelSprite.Update(gametime, curdir);
            count = count % 16;//Reset the count to prevent unnecessary storage usage.
                               //UPDATES positions and hitboxes
            prevPos = currPos;
            currPos = GelSprite.GetPos();

            prevHitbox = hitbox;
            hitbox.X = (int)currPos.X + (int)HitboxDims.X_ADJ;
            hitbox.Y = (int)currPos.Y + (int)HitboxDims.Y_ADJ;

            return null;


        }
        public void Draw()
        {
            drawHitbox();
            GelSprite.Draw(currPos);
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

