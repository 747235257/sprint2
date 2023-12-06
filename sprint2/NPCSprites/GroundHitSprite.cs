using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class GroundHitSprite: INPCSprite
    {

        private Texture2D texture;
        private const int width = 288;
        private const int height = 336;
        public Rectangle source;
        public Rectangle destination;
        private SpriteBatch spriteBatch;
        private enum dir { idle = 0, up = 1, down = 2, left = 3, right = 4 }

        public GroundHitSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            source = new Rectangle(0, 0, width, height);//The origin sprite frame.
            destination = new Rectangle(150, 200, 288, 336);
        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public Vector2 Update(GameTime gameTime, int curdir)
        {
            Vector2 updateMove = new Vector2(0, 0);
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
