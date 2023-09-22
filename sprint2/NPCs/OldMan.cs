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
    public class OldMan: INPC
    {
        public INPCSprite OldManSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        public OldMan(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            OldManSprite = new OldManSprite(this.texture, this.spriteBatch);
        }





        public void Attack()
        {


        }
        public void Stop()
        {
            
            OldManSprite = new OldManSprite(texture, spriteBatch);
        }

        public void Execute(GameTime gametime)
        {

           

            OldManSprite.Update(gametime, 0);
            


        }
        public void Draw()
        {
            OldManSprite.Draw();
        }

    }
}

