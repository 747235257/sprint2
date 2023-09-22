﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class GelSprite:INPCSprite
    {
      
        private Texture2D texture;
        private const int width = 8;
        private const int height = 16;
        public Rectangle source;
        public Rectangle destination = new Rectangle(200, 200, 16, 32);
        private SpriteBatch spriteBatch;
        private int frameCol;
        private enum dir { idle = 0, up = 1, down = 2, left = 3, right = 4 }
        private float timer;

        public GelSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            source = new Rectangle(1, 11, width, height);
            timer = 0;
            frameCol= 0;
        }
        public void Update(GameTime gameTime, int curdir)
        {
            
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
             if (timer > 0.1)
            {
                timer = 0;
                frameCol += 1;
                 switch (curdir)
                {
                    case (int)dir.idle:
                        break;
                    case (int)dir.up:
                        destination.Y -= 2;
                        break;
                    case (int)dir.down:
                        destination.Y += 2;
                        break;
                    case (int)dir.left:
                        destination.X -= 2;
                        break;
                    case (int)dir.right:
                        destination.X += 2;
                        break;
                    default:
                        break;

                }
                source.X = 1 + 9 * (frameCol % 2);
                frameCol %= 2;
                
            }



        }
        public void Draw()
        {

            spriteBatch.Begin();
            
            spriteBatch.Draw(texture, destination, source, Color.White);
            
            spriteBatch.End();

        }


    }
}