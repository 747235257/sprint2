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
        public Rectangle destination;
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffects;
        private enum dir { idle = 0,up = 1, down =2, left = 3, right =4}
        private bool flip;
        private int count;

        public SkullSprite(Texture2D texture, SpriteBatch spriteBatch) 
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            spriteEffects = SpriteEffects.FlipHorizontally;
            source = new Rectangle(1, 59, width, height);//The origin sprite frame.
            flip = false;
            destination = new Rectangle(200, 200, 32, 32);
            count = 0;
            
    }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public Rectangle Update(GameTime gameTime, int curdir)
        {
            
            
                count++;
                switch(curdir)
                {
                    case (int)dir.idle:
                        break;
                    case(int)dir.up:
                        destination.Y -= 2;//Speed is 2;
                    break;
                    case (int)dir.down:
                        destination.Y+= 2;
                        break;
                    case (int)dir.left:
                        destination.X-= 2;
                        break;
                    case(int)dir.right:
                        destination.X+= 2;
                        break;
                    default:
                        break;

                }
                if(count== 4)//Flip cool down.
                {
                 flip = !flip;
                }
                count %= 4;//Avoid too much use of storage.
             
                return destination;
            
            

        }
        public void Draw() {
            if (!flip)//Draw flip.
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0f, new Vector2(), spriteEffects, 1f);
            }
            else
            {
                spriteBatch.Draw(texture, destination, source, Color.White);
            }

        }
        
    }
}
