using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

public class RandomChest : IChest
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
    public List<IItem> items;
    private const int updateMax = 4;
    private int updateCounter;
    private const int sizeScale = 3;
    private SpriteBatch spriteBatch;
    private Game1 game1;
    private Game game;
    private enum HitboxDims
    {
        WIDTH = 50, HEIGHT = 40, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
    }
    public enum TextureDims
    {
        WIDTH = 16, HEIGHT = 16
    }

    private enum SpriteDims
    {
        ROWS = 1, COLS = 2
    }
    public RandomChest(Texture2D texture, int rows, int columns, Vector2 location, SpriteBatch spriteBatch, Game1 game1, Game game)
	{
        this.Texture = texture;
        this.Rows = rows;
        this.Columns = columns;
        currentFrame = 0;
        totalFrames = rows * columns;
        pos = location;
        updateCounter = 0;
        hitbox = new Rectangle((int)pos.X + (int)HitboxDims.X_ADJ, (int)pos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
        hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));

        this.spriteBatch = spriteBatch;
        this.game1 = game1;
        this.game = game;
        this.items = new List<IItem>();
        this.items.Add(new healthItem(new Vector2(pos.X, pos.Y + 10), game1, game1._spriteBatch));
        this.items.Add(new wep1(new Vector2(pos.X, pos.Y + 10), game1, game1._spriteBatch));
        this.items.Add(new wep2(new Vector2(pos.X, pos.Y + 10), game1, game1._spriteBatch));
        this.items.Add(new wep3(new Vector2(pos.X, pos.Y + 10), game1, game1._spriteBatch));
        this.items.Add(new mapItem(new Vector2(pos.X, pos.Y + 10), game1, game1._spriteBatch));
    }
    public void drawRandomChest()
    {
        int row = currentFrame / (int)SpriteDims.COLS;
        int col = currentFrame % (int)SpriteDims.COLS;


        Rectangle sourceRectangle = new Rectangle(0 + 17 * col, 0 + 17 * row, (int)TextureDims.WIDTH, (int)TextureDims.HEIGHT);
        Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)TextureDims.WIDTH * sizeScale, (int)TextureDims.HEIGHT * sizeScale);

        //drawHitbox();//for debugging
        //draws a portion of the texture into a portion of the screen
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
    }
    public void openChest(IChest chest, SpriteBatch spriteBatch)
    {
        Random rand = new Random();
        List<IItem> active = new List<IItem>();
        int item;
        int activeCount = 0;
        if(chest.isOpen())           //Check that when in range (touching the chest) it opens
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isAlive())
                {
                    active.Add(items[i]);
                    activeCount++;
                }
            }
            item = rand.Next(activeCount);
            spriteBatch.Begin();
            currentFrame++;
            active[item].Draw();
            spriteBatch.End();
        }
    }

    public Boolean isOpen()
    {
        int open = currentFrame;
        if(open != 0)
        {
            return false;
        }
        else return true;
    }
    public Rectangle getHitbox()
    {
        return hitbox;
    }
    public Boolean isTouching()
    {
        return true;
    }
}
