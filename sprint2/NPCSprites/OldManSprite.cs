using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class OldManSprite: INPCSprite
    {

        private Texture2D texture;
        private const int width = 16;
        private const int height = 16;
        public Rectangle source;
        public Rectangle destination;
        private SpriteBatch spriteBatch;
        public OldManSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            source = new Rectangle(1, 11, width, height);//The origin sprite frame.
            destination = new Rectangle(100, 200, 32, 32);
        }
        public Vector2 Update(GameTime gametime, int curdir)
        {
            return new Vector2(0, 0);
        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public void Draw(Vector2 pos)
        {
            destination.X = (int)pos.X;
            destination.Y = (int)pos.Y;
            spriteBatch.Draw(texture, destination, source, Color.White);
        }
    }
}
