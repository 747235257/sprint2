using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{

    public class Item
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int updateCounter;
        private const int updateMax = 5;
        private Vector2 pos;

        public Item(Texture2D texture, int rows, int columns, Vector2 location)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            pos = location;
        }

        private bool updateCheck() //updates the updateCounter and resets as needed
        {
            bool result = updateCounter < updateMax;
            updateCounter++;

            if(updateCounter > updateMax)
            {
                updateCounter = 0;
            }
            return !result;
        }
        public void CurrentItemPlus()
        {
            if (updateCheck()) //only switches after a time
            {
                if (currentFrame < totalFrames)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame = 0;
                }
            }
        }
        public void CurrentItemMinus()
        {
            if (updateCheck())
            {
                if (currentFrame != 0)
                {
                    currentFrame--;
                }
                else { currentFrame = totalFrames; }
            }
        }

        public void ItemProcess(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, width*2, height*2);

            //draws a portion of the texture into a portion of the screen
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();

        }
    }
}
