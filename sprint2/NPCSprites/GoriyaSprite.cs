using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
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
        private const int width = 64;
        private const int height = 64;
        public Rectangle source;
        public Rectangle destination;
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
            source = new Rectangle(0, 0, width, height);//The origin sprite frame.
            upDownflip = false;
            speed = 2;
            timer = 0;
            frameCol = 0;
            destination = new Rectangle(0, 0, 64, 64);


        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public Vector2 Update(GameTime gametime, int curdir)
        {
            Vector2 updateMove = new Vector2(0, 0);
            timer += (float)gametime.ElapsedGameTime.TotalSeconds;
           if(timer >0.1)//The cd of position update is 0.1 second.
            {
                timer= 0;
                switch (curdir)
                {
                    //Change the position and frame positions.
                    case (int)dir.idle:
                        if(source.X == 0||source.X == 0)
                        {
                            source.X = 64 * (frameCol % 2);
                            frameCol = (frameCol + 1) % 2;
                        }
                        break;

                    case (int)dir.up:

                        updateMove.Y = -speed;
                        source.X = 0; 
                        break;
                    case (int)dir.down:

                        updateMove.Y = speed;
                        source.X =0;
                        break;
                    case (int)dir.left:

                        updateMove.X = -speed;
                        source.X = 64* (frameCol % 2);
                        frameCol = (frameCol + 1) % 2;
                        leftRightflip = true;
                        break;
                    case (int)dir.right:

                        updateMove.X = +speed;
                        source.X = 64* (frameCol % 2);
                        frameCol = (frameCol + 1) % 2;
                        leftRightflip = false;

                        break;
                    default:
                        break;

                } 
                upDownflip = !upDownflip;
                
            }

           return updateMove;
            
            
        }

        public void Draw(Vector2 pos)
        {
            destination.X = (int)pos.X;
            destination.Y = (int)pos.Y;
            //Draw different sprites.

            if ((source.X == 256 || source.X == 273) && leftRightflip)//Left
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0f, new Vector2(), spriteEffects, 1f);
            }
            else if((source.X == 256 || source.X == 273) && !leftRightflip)//Right
            {
                spriteBatch.Draw(texture, destination, source, Color.White);
            }
            else if (upDownflip)
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0f, new Vector2(), spriteEffects, 1f);//Up or down flip.
            }
            else
            {
                spriteBatch.Draw(texture, destination, source, Color.White);// Up or down.
            }
        }
    }
}
