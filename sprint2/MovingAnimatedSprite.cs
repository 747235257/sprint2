﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


public class MovingAnimatedSprite : ISprite
{
    public Texture2D Texture { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    private int currentFrame;
    private int totalFrames;
    private int updateCounter = 0;
    private Vector2 pos;

    public MovingAnimatedSprite(Texture2D texture, int rows, int columns, Vector2 location)
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

        //frame updates every five updates
        updateCounter++;
        if (updateCounter > 5)
        {
            updateCounter = 0;
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        this.pos.X += 3;
        //resets position of sprite when it moves too far
        //if (this.pos.X > _graphics.PreferredBackBufferWidth)
        //    this.pos.X = 1;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 loc)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = currentFrame / Columns;
        int column = currentFrame % Columns;

        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, width, height);

        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

    }

    public void DrawHitbox(SpriteBatch spriteBatch, Vector2 loc, Rectangle hitbox)
    {
        spriteBatch.Draw(Texture, hitbox, Color.AliceBlue);
    }

}
