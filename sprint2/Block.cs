using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Block : IBlock
{
    public Texture2D Texture { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    private int currentFrame;
    private int totalFrames;
    //private int updateCounter = 0;
    private Vector2 pos;

    public Block(Texture2D texture, int rows, int columns, Vector2 location)
    {
        this.Texture = texture;
        this.Rows = rows;
        this.Columns = columns;
        currentFrame = 0;
        totalFrames = (rows * columns) - 2;
        pos = location;
    }
    public enum FrameDirection
    {
        Forward, Backward
    }
    public void switchBlock(GraphicsDeviceManager _graphics, FrameDirection direction)
    {
        if (direction == FrameDirection.Forward)
        {
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }
        }
        else if (direction == FrameDirection.Backward)
        {
            currentFrame--;
            if (currentFrame < 0)
            {
                currentFrame = totalFrames - 1;
            }
        }
    } 
    public void blockPosition(Vector2 position)
    {

    }
    public void Update(GraphicsDeviceManager _graphics)
    {
        currentFrame++;
    }

    public void drawBlock(SpriteBatch spriteBatch)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = currentFrame / Columns;
        int column = currentFrame % Columns;

        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Rectangle destinationRectangle = new Rectangle((int)pos.X - 70, (int)pos.Y - 100, width * 5, height * 5);

        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
    }
}
