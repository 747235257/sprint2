using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace sprint2
{
    public class DragonSprite: INPCSprite
    {
        private Texture2D texture;
        private const int width = 64;
        private const int height = 128;
        public Rectangle source;
        public Rectangle destination;
        private SpriteBatch spriteBatch;
        
        private int frameCol;
        private enum dir { idle = 0, left = 1, right = 2 }
        
        private float timer;

        public DragonSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            source = new Rectangle(0, 0, width, height);//The origin sprite frame.
            timer = 0;
            frameCol = 0;
            destination = new Rectangle(0, 0, 64, 128);
        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public Vector2 Update(GameTime gameTime,int curdir)
        {
            Vector2 updateMove = new Vector2(0, 0);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
             if (timer > 0.1)//The cd of position update is 0.1 second.
            {
                timer = 0;
                frameCol += 1;
                switch (curdir)
                {
                    case (int)dir.idle:
                        break;
                    
                    case (int)dir.left:
                        updateMove.X =  -2;//Speed is 2;
                        break;
                    case (int)dir.right:
                        updateMove.X =  2;
                        break;
                    default:
                        break;

                }

                SoundEffectInstance walkingSound = SoundManager.Instance.CreateSound("walking");
                walkingSound.Play();
                source.X = 64* (frameCol % 4);//Change the sprite source position in a spritesheet.

                source.X = 1 + 25 * (frameCol % 4);//Change the sprite source position in a spritesheet.

                frameCol %= 4;//Total frame number is 4.

            }
            return updateMove;
        }
        public void Draw(Vector2 pos)
        {
            destination.X = (int)pos.X;
            destination.Y = (int)pos.Y;
            spriteBatch.Draw(texture, destination, source, Color.White);


        }
    }
}
