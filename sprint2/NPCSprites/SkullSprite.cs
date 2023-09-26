﻿using System;
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
            source = new Rectangle(1, 59, width, height);
            flip= false;
            destination = new Rectangle(200, 200, 32, 32);
            count = 0;
            
    }
        public Rectangle Update(GameTime gameTime, int curdir)
        {
            
            
                count++;
                switch(curdir)
                {
                    case (int)dir.idle:
                        break;
                    case(int)dir.up:
                        destination.Y -= 2;
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
                if(count== 3)
                {
                 flip = !flip;
                }
                count %= 4;
             
                return destination;
            
            

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
