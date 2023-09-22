﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class GoriyaSprite: INPCSprite
    {

        private Texture2D texture;
        private const int width = 16;
        private const int height = 16;
        public Rectangle source;
        public Rectangle destination = new Rectangle(200, 200, 32, 32);
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffects;
        private enum dir { idle = 0, up = 1, down = 2, left = 3, right = 4 }
        private int frameCol;
        private bool upDownflip;
        private bool leftRightflip;
        private int speed;
        private float timer;
        public GoriyaSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            spriteEffects = SpriteEffects.FlipHorizontally;
            source = new Rectangle(222, 11, width, height);
            upDownflip= false;
            speed = 2;
            timer = 0;
            frameCol = 0;
            
            
        }
        public void Update(GameTime gametime, int curdir)
        {
            timer += (float)gametime.ElapsedGameTime.TotalSeconds;
           if(timer >0.1)
            {
                timer= 0;
                switch (curdir)
                {
                    case (int)dir.idle:
                        if(source.X == 256||source.X == 273)
                        {
                            source.X = 256 + 17 * (frameCol % 2);
                            frameCol = (frameCol + 1) % 2;
                        }
                        break;

                    case (int)dir.up:

                        destination.Y -= speed;
                        source.X = 239; 
                        break;
                    case (int)dir.down:

                        destination.Y += speed;
                        source.X = 222;
                        break;
                    case (int)dir.left:

                        destination.X -= speed;
                        source.X = 256 + 17 * (frameCol % 2);
                        frameCol = (frameCol + 1) % 2;
                        leftRightflip = true;
                        break;
                    case (int)dir.right:

                        destination.X += speed;
                        source.X = 256 + 17 * (frameCol % 2);
                        frameCol = (frameCol + 1) % 2;
                        leftRightflip = false;

                        break;
                    default:
                        break;

                } 
                upDownflip = !upDownflip;
                
            }
            
            
        }

        public void Draw()
        {
            spriteBatch.Begin();
            if ((source.X == 256 || source.X == 273) && leftRightflip)
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0f, new Vector2(), spriteEffects, 1f);
            }
            else if((source.X == 256 || source.X == 273) && !leftRightflip)
            {
                spriteBatch.Draw(texture, destination, source, Color.White);
            }
            else if (upDownflip)
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