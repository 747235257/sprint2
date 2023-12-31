﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class OldMan: INPC
    {
        public INPCSprite OldManSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private Game game;
        private bool isAlive;

        private enum HitboxDims
        {
            WIDTH = 32, HEIGHT = 32, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public OldMan(Texture2D texture, SpriteBatch spriteBatch, Game game, Vector2 startPos)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            OldManSprite = new OldManSprite(this.texture, this.spriteBatch);
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
            return null;

        }
        public void Stop()
        {
            
            OldManSprite = new OldManSprite(texture, spriteBatch);
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

           

            Vector2 updateMove = OldManSprite.Update(gametime, 0);
            currPos.X += updateMove.X;
            currPos.Y += updateMove.Y;

            //UPDATES positions and hitboxes
            prevPos = currPos;

            prevHitbox = hitbox;
            hitbox.X = (int)currPos.X + (int)HitboxDims.X_ADJ;
            hitbox.Y = (int)currPos.Y + (int)HitboxDims.Y_ADJ;
            return null;
            


        }
        public void Draw()
        {
            //drawHitbox();
            OldManSprite.Draw(currPos);
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
        public Vector2 getLastPos()
        {
            return currPos;
        }
    }
}

