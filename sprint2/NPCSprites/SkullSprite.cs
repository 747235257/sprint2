using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace sprint2
{
    public class SkullSprite: INPCSprite

    {
        private Texture2D texture;
        private const int width = 16;
        private const int height = 16;
        public Rectangle source;
        public Rectangle destination = new Rectangle(200, 200, 32, 32);
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffects;
        private enum dir { idle = 0,up = 1, down =2, left = 3, right =4}
        private bool flip;

        public SkullSprite(Texture2D texture, SpriteBatch spriteBatch) 
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            spriteEffects = SpriteEffects.FlipHorizontally;
            source = new Rectangle(1, 59, width, height);
            flip= false;
        }
        public void Update(GameTime gameTime, int curdir)
        {
            
            
            
                switch(curdir)
                {
                    case (int)dir.idle:
                        break;
                    case(int)dir.up:
                        destination.Y -= 8;
                        break;
                    case (int)dir.down:
                        destination.Y+= 8;
                        break;
                    case (int)dir.left:
                        destination.X-= 8;
                        break;
                    case(int)dir.right:
                        destination.X+= 8;
                        break;
                    default:
                        break;

                }
                flip = !flip;
             
            
            

        }
        public void Draw() {

            spriteBatch.Begin();
            if (!flip)
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0f, new Vector2(), spriteEffects, 1f);
            }
            else
            {
                spriteBatch.Draw(texture, destination, source, Color.White);
            }

            spriteBatch.End();

        }
        
    }
}
