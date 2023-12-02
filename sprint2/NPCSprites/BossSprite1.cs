using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class BossSprite1: INPCSprite
    {
        private Texture2D texture;
        private const int width = 74;
        private const int height = 48;
        public Rectangle source;
        public Rectangle destination;
        private SpriteBatch spriteBatch;
        private enum attack { leftAttack = 0, rightAttack = 1, middileBeam = 2, bulletHell = 3}

        public BossSprite1(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            source = new Rectangle(0, 0, width, height);//The origin sprite frame.
            destination = new Rectangle(150, 200, 74*4, 48*4);
        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public Vector2 Update(GameTime gameTime, int curdir)
        {
            Vector2 updateMove = new Vector2(0, 0);
            //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (timer > 0.1)//The cd of position update is 0.1 second.
            //{
            //    timer = 0;
            //    frameCol += 1;
            //    switch (curdir)
            //    {
            //        case (int)dir.idle:
            //            break;
            //        case (int)dir.up:
            //            updateMove.Y = -2;//Speed is 2;
            //            break;
            //        case (int)dir.down:
            //            updateMove.Y = 2;
            //            break;
            //        case (int)dir.left:
            //            updateMove.X = -2;
            //            break;
            //        case (int)dir.right:
            //            updateMove.X = 2;
            //            break;
            //        default:
            //            break;

            //    }
            //    source.X = 183 + 17 * (frameCol % 2);//Change the sprite source position in a spritesheet.
            //    frameCol %= 2;//Total frame number is 2.

            //}
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
