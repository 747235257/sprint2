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
        public Gel(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            GelSprite = new GelSprite(this.texture, this.spriteBatch);
            count = 0;
        }





        public void Attack()
        {


        }
        public void Stop()
        {
            count = 0;
            GelSprite = new GelSprite(texture, spriteBatch);
        }

        public void Execute(GameTime gametime)
        {

            count++;
            
            if (count % 16 == 0)
            {
                curdir = rnd.Next(0, 5);
                

            }

            GelSprite.Update(gametime, curdir);
            count = count % 16;


        }
        public void Draw()
        {
            GelSprite.Draw();
        }

    }
}

