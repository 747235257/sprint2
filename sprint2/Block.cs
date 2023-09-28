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
            
            
             currentFrame = (currentFrame+1) %10;
            
        }
        else if (direction == FrameDirection.Backward)
        {
            
            
             currentFrame = (currentFrame+9)%10;
            
        }
    } 
    public void blockPosition(Vector2 position)
    {

    }
    

    public void drawBlock(SpriteBatch spriteBatch)
    {
        int width = 16;
        int height = 16;
        int row = currentFrame / 4 ;
        int col = currentFrame % 4;
        

        Rectangle sourceRectangle = new Rectangle(0+17*col, 0+17*row, width, height);
        Rectangle destinationRectangle = new Rectangle((int)pos.X - 70, (int)pos.Y - 100, width * 5, height * 5);

        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
    }
}
