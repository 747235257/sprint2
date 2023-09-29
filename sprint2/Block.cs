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
    private const int updateMax = 4;
    private int updateCounter;
    private const int sizeScale = 3;

    public Block(Texture2D texture, int rows, int columns, Vector2 location)
    {
        this.Texture = texture;
        this.Rows = rows;
        this.Columns = columns;
        currentFrame = 0;
        totalFrames = (rows * columns) - 2;
        pos = location;
        updateCounter = 0;
    }
    public enum FrameDirection
    {
        Forward, Backward
    }

    private enum TextureDims
    {
        WIDTH = 16, HEIGHT = 16
    }

    private enum SpriteDims
    {
        ROWS = 3, COLS = 4
    }

    private bool updateCheck()
    {
        bool result = updateCounter < updateMax;
        updateCounter++;

        if (updateCounter > updateMax)
        {
            updateCounter = 0;
        }
        return !result;
    }

    public void switchBlock(GraphicsDeviceManager _graphics, FrameDirection direction)
    {
        if (updateCheck())
        {
            if (direction == FrameDirection.Forward)
            {


                currentFrame = (currentFrame + 1) % totalFrames;

            }
            else if (direction == FrameDirection.Backward)
            {


                currentFrame = (currentFrame + 9) % totalFrames;

            }
        }
    } 
    public void blockPosition(Vector2 position)
    {

    }
    

    public void drawBlock(SpriteBatch spriteBatch)
    {
        int row = currentFrame / (int)SpriteDims.COLS ;
        int col = currentFrame % (int)SpriteDims.COLS;
        

        Rectangle sourceRectangle = new Rectangle(0+17*col, 0+17*row, (int)TextureDims.WIDTH, (int)TextureDims.HEIGHT);
        Rectangle destinationRectangle = new Rectangle((int)pos.X - 70, (int)pos.Y - 100, (int)TextureDims.WIDTH * sizeScale, (int)TextureDims.HEIGHT * sizeScale);

        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
    }
}
