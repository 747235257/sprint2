using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class NonMoveAnimatedSprite : ISprite
{
    public Texture2D Texture { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    private int currentFrame;
    private int totalFrames;
    private int updateCounter = 0;
    private Vector2 pos;

    public NonMoveAnimatedSprite(Texture2D texture, int rows, int columns, Vector2 location)
    {
        Texture = texture;
        Rows = rows;
        Columns = columns;
        currentFrame = 0;
        totalFrames = Rows * Columns;
        pos = location;
    }

    public void Update()
    {

        //Only goes to next frame every five updates
        updateCounter++;
        if (updateCounter > 3)
        {
            updateCounter = 0;
            currentFrame++;
            if (currentFrame == totalFrames) //resets to current frame
                currentFrame = 0;
        }

    }

    public void Draw(SpriteBatch spriteBatch, Vector2 loc)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = currentFrame / Columns;
        int column = currentFrame % Columns;

        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Rectangle destinationRectangle = new Rectangle((int)loc.X, (int)loc.Y, width, height);

        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

    }
}
