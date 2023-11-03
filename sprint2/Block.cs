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
    private Rectangle hitbox; //COLLISION SPRINT3
    private ISprite hitboxSprite; //COLLISION SPRINT3
    private const int updateMax = 4;
    private int updateCounter;
    private const int sizeScale = 3;
    private SpriteBatch spriteBatch;
    private Game game;

    private enum HitboxDims
    {
        WIDTH = 50, HEIGHT = 50, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
    }
    public Block(Texture2D texture, int rows, int columns, Vector2 location, SpriteBatch spriteBatch, Game game)
    {
        this.Texture = texture;
        this.Rows = rows;
        this.Columns = columns;
        currentFrame = 0;
        totalFrames = (rows * columns) - 2;
        pos = location;
        updateCounter = 0;
        hitbox = new Rectangle((int)pos.X + (int)HitboxDims.X_ADJ, (int)pos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
        hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));

        this.spriteBatch = spriteBatch;
        this.game = game;
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
    public Vector2 blockPosition(Vector2 position)
    {
        return pos;
    }
    

    public void drawBlock()
    {
        int row = currentFrame / (int)SpriteDims.COLS ;
        int col = currentFrame % (int)SpriteDims.COLS;
        

        Rectangle sourceRectangle = new Rectangle(0+17*col, 0+17*row, (int)TextureDims.WIDTH, (int)TextureDims.HEIGHT);
        Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)TextureDims.WIDTH * sizeScale, (int)TextureDims.HEIGHT * sizeScale);

        //drawHitbox();//for debugging
        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void drawHitbox()
    {
        hitboxSprite.DrawHitbox(spriteBatch, new Vector2(hitbox.X, hitbox.Y), hitbox);
    }
    public Rectangle getHitbox()
    {
        return hitbox;
    }

}
