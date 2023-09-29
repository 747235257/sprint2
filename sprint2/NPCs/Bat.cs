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
    public class Bat: INPC
    {
        public INPCSprite BatSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        public Bat(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            BatSprite = new BatSprite(this.texture, this.spriteBatch);
            count = 0;
            curdir = 0;
        }





        public List<IProjectile> Attack()
        {
            return null;

        }
        public void Stop()
        {
            //Reset the npc
            count = 0;
            BatSprite = new BatSprite(texture, spriteBatch);
            curdir= 0;
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

            count++;
            
            if (count % 16 == 0)//Interval of a diraction generator. 
            {
                curdir = rnd.Next(0, 5);
                

            }

            BatSprite.Update(gametime, curdir);
            count = count % 16;//Reset the count to prevent unnecessary storage usage.

            return null;


        }
        public void Draw()
        {
            BatSprite.Draw();
        }

    }

}
