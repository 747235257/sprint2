using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private float duration;
        private bool attack;
        public Goriya(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            GoriyaSprite = new GoriyaSprite(this.texture, this.spriteBatch);
            count = 0;
            duration = 0;
            attack = false;
        }





        public void Attack()
        {


        }
        public void Stop()
        {
            count = 0;
            duration = 0;
            GoriyaSprite = new GoriyaSprite(texture, spriteBatch);
        }

        public void Execute(GameTime gametime)
        {

            count++;
            if (attack)
            {
                duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (duration == 0)
                {
                    Attack();
                }
                else if (duration > 2)
                {
                    duration = 0;
                    attack = false;
                }
                else
                {
                    return;
                }
            }
            if (count % 16 == 0)
            {
                curdir = rnd.Next(0, 5);
                if (curdir == 0)
                {
                    attack = true;
                }

            }

            GoriyaSprite.Update(gametime, curdir);
            count = count % 16;


        }
        public void Draw()
        {
            GoriyaSprite.Draw();
        }
    }
}
