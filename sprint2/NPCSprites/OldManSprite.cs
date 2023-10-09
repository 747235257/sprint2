﻿using Microsoft.Xna.Framework;
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
            destination = new Rectangle(200, 200, 32, 32);
        }
        public Rectangle Update(GameTime gametime, int curdir)
        {
            return destination;
        }

        //returns the current position of the enemy on screen
        public Vector2 GetPos()
        {
            return new Vector2(destination.X, destination.Y);
        }
        public void Draw()
        {
            spriteBatch.Draw(texture, destination, source, Color.White);
        }
    }
}
