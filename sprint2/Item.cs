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
    }
}
