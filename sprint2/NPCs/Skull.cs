using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace sprint2
{
    public class Skull: INPC
    {
        public INPCSprite SkullSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        public Skull(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            SkullSprite = new BatSprite(this.texture, this.spriteBatch);
            count = 0;
            curdir = 0;
        }





        public List<IProjectile> Attack()
        {
            return null;

        }
        public void Stop()
        {
            count = 0;
            SkullSprite = new SkullSprite(texture, spriteBatch);
            curdir = 0;
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

            count++;

            if (count % 16 == 0)
            {
                curdir = rnd.Next(0, 5);


            }

            SkullSprite.Update(gametime, curdir);
            count = count % 16;

            return null;
        }
        public void Draw()
        {
            SkullSprite.Draw();
        }

        
    }
}

