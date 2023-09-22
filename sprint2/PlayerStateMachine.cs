using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

public class PlayerStateMachine : IPlayerStateMachine
{

    private enum State
    {
        IDLE_DOWN, IDLE_UP, IDLE_LEFT, IDLE_RIGHT,
        ATTACK_DOWN, ATTACK_UP, ATTACK_LEFT, ATTACK_RIGHT,
        MOVE_DOWN, MOVE_UP, MOVE_LEFT, MOVE_RIGHT,
        ITEM_DOWN, ITEM_UP, ITEM_LEFT, ITEM_RIGHT,
        DAMAGED_DOWN, DAMAGED_UP, DAMAGED_LEFT, DAMAGED_RIGHT
    }
    State state;
    private Vector2 currPos;
    private List<ISprite> walkSprites;
    private List<ISprite> idleSprites;
    private List<ISprite> attackSprites;
    private List<ISprite> itemSprites;
    private List<ISprite> damagedSprites;
    private ISprite currSprite;
    private GraphicsDeviceManager graphics;
    private Game game;
    private SpriteBatch spriteBatch;
    private int attackCounter;
    private int itemCounter;

    private void LoadContent(Game game)
    {
        //initialize empty lists
        idleSprites = new List<ISprite>();
        attackSprites = new List<ISprite>();
        itemSprites = new List<ISprite>();
        damagedSprites = new List<ISprite>();
        walkSprites = new List<ISprite>();
        //idle sprites
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleDown"), 1, 1, this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleUp"), 1, 1, this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleLeft"), 1, 1, this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleRight"), 1, 1, this.currPos));
        //attack sprites
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackDown"), 1, 3, this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackUp"), 1, 3, this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackLeft"), 1, 3, this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackRight"), 1, 3, this.currPos));
        //walkSprites
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveDown"), 1, 4, this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveUp"), 1, 4, this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveLeft"), 1, 4, this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveRight"), 1, 4, this.currPos));
        //damagedSprites
        damagedSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("DamagedDown"), 1, 1, this.currPos));
        //itemSprites
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemDown"), 1, 1, this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemUp"), 1, 1, this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemLeft"), 1, 1, this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemRight"), 1, 1, this.currPos));
    }

    public void drawCurrentSprite()
    {
        //spriteBatch.Begin();
        currSprite.Draw(spriteBatch, currPos); //draws current sprite
        //spriteBatch.End();
    }
    public PlayerStateMachine(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
    {
        //graphics and game are set
        this.graphics = graphics;
        this.game = game;
        this.spriteBatch = spriteBatch;

        this.attackCounter = 0;
        this.itemCounter = 0;

        currPos = new Vector2(50, 50); //initial pos
        LoadContent(game);
        state = State.IDLE_DOWN; //initial state
        currSprite = idleSprites[0]; //initial current sprite
    }

    public bool InAttack()
    {
        return attackCounter > 0;
    }

    public bool InItem()
    {
        return itemCounter > 0;
    }
    public bool IsIdle()
    {
        return (state == State.IDLE_DOWN || state == State.IDLE_UP || state == State.IDLE_DOWN || state == State.IDLE_DOWN);
    }
    public void setIdle()
    {

        if (!InAttack() && !InItem())
        {
            if (state == State.MOVE_DOWN || state == State.ATTACK_DOWN || state == State.ITEM_DOWN || state == State.DAMAGED_DOWN)
            {
                currSprite = idleSprites[0];
                state = State.IDLE_DOWN;
            }
            else if (state == State.MOVE_UP || state == State.ATTACK_UP || state == State.ITEM_UP || state == State.DAMAGED_UP)
            {
                currSprite = idleSprites[1];
                state = State.IDLE_UP;
            }
            else if (state == State.MOVE_RIGHT || state == State.ATTACK_RIGHT || state == State.ITEM_RIGHT || state == State.DAMAGED_RIGHT)
            {
                currSprite = idleSprites[3];
                state = State.IDLE_RIGHT;
            }
            else if (state == State.MOVE_LEFT || state == State.ATTACK_LEFT || state == State.ITEM_LEFT || state == State.DAMAGED_LEFT)
            {
                currSprite = idleSprites[2];
                state = State.IDLE_LEFT;
            }
        }
    }
    public void moveLeft()
    {

        if (!InAttack() && !InItem())
        {
            currPos.X -= 4; //updates the position
            if (state == State.MOVE_LEFT) //if alr left, updates animation
            {
                currSprite.Update();
            }
            else //else gets left walk cycle
            {
                currSprite = walkSprites[2];
            }

            state = State.MOVE_LEFT;
        }
    }

    public void moveRight()
    {
        if (!InAttack() && !InItem())
        {
            currPos.X += 4; //updates the position
            if (state == State.MOVE_RIGHT) //if alr left, updates animation
            {
                currSprite.Update();
            }
            else //else gets left walk cycle
            {
                currSprite = walkSprites[3];
            }

            state = State.MOVE_RIGHT;
        }
    }

    public void moveDown()
    {
        if (!InAttack() && !InItem())
        {
            currPos.Y += 4; //updates the position
            if (state == State.MOVE_DOWN) //if alr left, updates animation
            {
                currSprite.Update();
            }
            else //else gets left walk cycle
            {
                currSprite = walkSprites[0];
            }

            state = State.MOVE_DOWN;
        }
    }

    public void moveUp()
    {
        if (!InAttack() && !InItem())
        {
            currPos.Y -= 4; //updates the position
            if (state == State.MOVE_UP) //if alr left, updates animation
            {
                currSprite.Update();
            }
            else//else gets left walk cycle
            {
                currSprite = walkSprites[1];
            }

            state = State.MOVE_UP;
        }
    }

    public void setDamaged()
    {

        if (state == State.MOVE_DOWN || state == State.ATTACK_DOWN || state == State.ITEM_DOWN || state == State.IDLE_DOWN)
        {
            currSprite = damagedSprites[0];
            state = State.DAMAGED_DOWN;
        }
        else if (state == State.MOVE_UP || state == State.ATTACK_UP || state == State.ITEM_UP || state == State.IDLE_UP)
        {
            currSprite = damagedSprites[0];
            state = State.DAMAGED_UP;
        }
        else if (state == State.MOVE_RIGHT || state == State.ATTACK_RIGHT || state == State.ITEM_RIGHT || state == State.IDLE_RIGHT)
        {
            currSprite = damagedSprites[0];
            state = State.DAMAGED_RIGHT;
        }
        else if (state == State.MOVE_LEFT || state == State.ATTACK_LEFT || state == State.ITEM_LEFT || state == State.IDLE_LEFT)
        {
            currSprite = damagedSprites[0];
            state = State.DAMAGED_LEFT;
        }
    }

    public Vector2 attack()
    {
        Vector2 rangeAttack = currPos;

        if (!InAttack() && !InItem())
        {
            if (state == State.IDLE_DOWN)
            {
                rangeAttack.Y += 1;
                currSprite = attackSprites[0];
                state = State.ATTACK_DOWN;
                attackCounter = 1;
            }
            else if (state == State.IDLE_UP)
            {
                rangeAttack.Y -= 1;
                currSprite = attackSprites[1];
                state = State.ATTACK_UP;
                attackCounter = 1;
            }
            else if (state == State.IDLE_LEFT)
            {
                rangeAttack.X -= 1;
                currSprite = attackSprites[2];
                state = State.ATTACK_LEFT;
                attackCounter = 1;
            }
            else if (state == State.IDLE_RIGHT)
            {
                rangeAttack.X += 1;
                currSprite = attackSprites[3];
                state = State.ATTACK_RIGHT;
                attackCounter = 1;
            }
        }


        return rangeAttack;
    }

    public void updateAttack()
    {

        if (attackCounter > 0)
        {
            attackCounter++;
            currSprite.Update();
        }
        if (attackCounter > 25)
        {
            attackCounter = 0;
            setIdle();
        }

    }

    public void updateItem()
    {

        if (itemCounter > 0) itemCounter++;
        if (itemCounter > 25)
        {
            itemCounter = 0;
            setIdle();
        }

    }

    public int useItem()
    {
        if (!InAttack() && !InItem())
        {
            if (state == State.IDLE_DOWN)
            {
                currSprite = itemSprites[0];
                state = State.ITEM_DOWN;
                itemCounter = 1;
            }
            else if (state == State.IDLE_UP)
            {
                currSprite = itemSprites[1];
                state = State.ITEM_UP;
                itemCounter = 1;
            }
            else if (state == State.IDLE_LEFT)
            {
                currSprite = itemSprites[2];
                state = State.ITEM_LEFT;
                itemCounter = 1;
            }
            else if (state == State.IDLE_RIGHT)
            {
                currSprite = itemSprites[3];
                state = State.ITEM_RIGHT;
                itemCounter = 1;
            }
            //something = new Projectile();
            // return new Projectile(,....)
        }
        return 0;

    }

}
