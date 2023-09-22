using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class Dragon:INPC
    {
        public INPCSprite DragonSprite{get; set;}
        private Texture2D texture { get; set;}
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private float duration;
        private bool attack;
        public Dragon(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            DragonSprite = new DragonSprite(this.texture, this.spriteBatch);
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
            DragonSprite = new DragonSprite(texture, spriteBatch);
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
                curdir = rnd.Next(0, 3);
                if(curdir == 0)
                {
                    attack= true;
                }
                
            } 
            
            DragonSprite.Update(gametime, curdir);
            count = count % 16;
            
            
        }
        public void Draw()
        {
            DragonSprite.Draw();
        }
        
    }
}
