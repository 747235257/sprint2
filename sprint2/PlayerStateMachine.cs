using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using sprint2;
using System.Security.Cryptography.X509Certificates;

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

    private enum DirNums //differentiates directions in arrays
    {
        DOWN = 0, UP = 1, LEFT = 2, RIGHT = 3
    }

    private enum TextureDims //dimensions of textures
    {
        IDLE_R = 1, IDLE_C = 1,
        ATTACK_R = 1, ATTACK_C = 3,
        WALK_R = 1, WALK_C = 4,
        DAMAGED_R = 1, DAMAGED_C = 1,
        ITEM_R = 1, ITEM_C = 1,

    }

    private enum PosNums
    {
        START_X = 50, START_Y = 50, MOV_RANGE = 4
    }

    private enum MaxFrames
    {
        MAX_ATTACK = 25, MAX_ITEM = 25, MAX_DAMAGED = 30
    }

    private enum PlayerTextureDims
    {
        WIDTH = 87, HEIGHT = 99
    }

    //COLLISION SPRINT3
    private enum HitboxDims
    {
        WIDTH = 45, HEIGHT = 45, X_ADJ = 20, Y_ADJ = 25, ROW = 1, COL = 1
    }

    State state;
    private Vector2 currPos;
    private Vector2 prevPos;
    private Rectangle hitbox; //COLLISION SPRINT3
    private Rectangle prevHitbox;
    private ISprite hitboxSprite; //COLLISION SPRINT3
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
    private int damagedCounter;
    private ProjectileFactory factory = new ProjectileCreator();

    private void LoadContent(Game game)
    {
        //initialize empty lists
        idleSprites = new List<ISprite>();
        attackSprites = new List<ISprite>();
        itemSprites = new List<ISprite>();
        damagedSprites = new List<ISprite>();
        walkSprites = new List<ISprite>();  
        //idle sprites
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleDown"), ((int)TextureDims.IDLE_R), ((int)TextureDims.IDLE_C), this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleUp"), ((int)TextureDims.IDLE_R), ((int)TextureDims.IDLE_C), this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleLeft"), ((int)TextureDims.IDLE_R), ((int)TextureDims.IDLE_C), this.currPos));
        idleSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("IdleRight"), ((int)TextureDims.IDLE_R), ((int)TextureDims.IDLE_C), this.currPos));
        //attack sprites
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackDown"), ((int)TextureDims.ATTACK_R), ((int)TextureDims.ATTACK_C), this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackUp"), ((int)TextureDims.ATTACK_R), ((int)TextureDims.ATTACK_C), this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackLeft"), ((int)TextureDims.ATTACK_R), ((int)TextureDims.ATTACK_C), this.currPos));
        attackSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("AttackRight"), ((int)TextureDims.ATTACK_R), ((int)TextureDims.ATTACK_C), this.currPos));
        //walkSprites
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveDown"), ((int)TextureDims.WALK_R), ((int)TextureDims.WALK_C), this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveUp"), ((int)TextureDims.WALK_R), ((int)TextureDims.WALK_C), this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveLeft"), ((int)TextureDims.WALK_R), ((int)TextureDims.WALK_C), this.currPos));
        walkSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("MoveRight"), ((int)TextureDims.WALK_R), ((int)TextureDims.WALK_C), this.currPos));
        //damagedSprites
        damagedSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("DamagedDown"), ((int)TextureDims.DAMAGED_R), ((int)TextureDims.DAMAGED_C), this.currPos));
        damagedSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("DamagedUp"), ((int)TextureDims.DAMAGED_R), ((int)TextureDims.DAMAGED_C), this.currPos));
        damagedSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("DamagedLeft"), ((int)TextureDims.DAMAGED_R), ((int)TextureDims.DAMAGED_C), this.currPos));
        damagedSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("DamagedRight"), ((int)TextureDims.DAMAGED_R), ((int)TextureDims.DAMAGED_C), this.currPos));
        //itemSprites
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemDown"), ((int)TextureDims.ITEM_R), ((int)TextureDims.ITEM_C), this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemUp"), ((int)TextureDims.ITEM_R), ((int)TextureDims.ITEM_C), this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemLeft"), ((int)TextureDims.ITEM_R), ((int)TextureDims.ITEM_C), this.currPos));
        itemSprites.Add(new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("ItemRight"), ((int)TextureDims.ITEM_R), ((int)TextureDims.ITEM_C), this.currPos));

        //COLLISION SPRINT3
        hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
    }

    public void drawCurrentSprite()
    {
        //spriteBatch.Begin
        drawHitbox(); //COLLISION SPRINT3
        currSprite.Draw(spriteBatch, currPos); //draws current sprite
        //spriteBatch.End();
    }

    //COLLISION SPRINT3
    public void drawHitbox()
    {
        hitboxSprite.DrawHitbox(spriteBatch, new Vector2(hitbox.X, hitbox.Y), hitbox);
    }

    //COLLISION SPRINT3
    public Rectangle getHitbox()
    {
        return hitbox;
    }

    public PlayerStateMachine(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 startPos)
	{
        //graphics and game are set
        this.graphics = graphics;
        this.game = game;
        this.spriteBatch = spriteBatch;

        this.attackCounter = 0;
        this.itemCounter = 0;

        currPos = startPos;
        prevPos = currPos;
        //COLLISION SPRINT3
        hitbox = new Rectangle((int)currPos.X + (int)HitboxDims.X_ADJ, (int)currPos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
        prevHitbox = hitbox;
        LoadContent(game);
        state = State.IDLE_DOWN; //initial state
        currSprite = idleSprites[(int)DirNums.DOWN]; //initial current sprite
    }

    private bool InAttack()
    {
        return attackCounter > 0;
    }

    private bool InItem()
    {
        return itemCounter > 0;
    }

    public bool InDamaged()
    {
        return damagedCounter > 0;
    }
    public bool IsIdle()
    {
        return (state == State.IDLE_DOWN || state == State.IDLE_UP || state == State.IDLE_DOWN || state == State.IDLE_DOWN);
    }

    private bool isDown()
    {
        return (state == State.MOVE_DOWN || state == State.ATTACK_DOWN || state == State.ITEM_DOWN || state == State.DAMAGED_DOWN || state == State.IDLE_DOWN);
    }

    private bool isUp()
    {
        return (state == State.MOVE_UP || state == State.ATTACK_UP || state == State.ITEM_UP || state == State.DAMAGED_UP || state == State.IDLE_UP);
    }

    private bool isRight()
    {
        return (state == State.MOVE_RIGHT || state == State.ATTACK_RIGHT || state == State.ITEM_RIGHT || state == State.DAMAGED_RIGHT || state == State.IDLE_RIGHT);
    }
    private bool isLeft()
    {
        return (state == State.MOVE_LEFT || state == State.ATTACK_LEFT || state == State.ITEM_LEFT || state == State.DAMAGED_LEFT || state == State.IDLE_LEFT);
    }

    public void setLocation(Vector2 pos)
    {
        this.currPos = pos;
        this.prevPos = pos;

        this.hitbox.X = (int)pos.X + (int)HitboxDims.X_ADJ;
        this.hitbox.Y = (int)pos.Y + (int)HitboxDims.Y_ADJ;

        this.prevHitbox = this.hitbox;
    }
    public void setIdle()
    {

        if (!InAttack() && !InItem() && !InDamaged())
        {
            //idle sprite is set depending on last dir
            if (isDown())
            {
                currSprite = idleSprites[(int)DirNums.DOWN];
                state = State.IDLE_DOWN;
            }
            else if (isUp())
            {
                currSprite = idleSprites[(int)DirNums.UP];
                state = State.IDLE_UP;
            }
            else if (isRight())
            {
                currSprite = idleSprites[(int)DirNums.RIGHT];
                state = State.IDLE_RIGHT;
            }
            else if (isLeft())
            {
                currSprite = idleSprites[(int)DirNums.LEFT];
                state = State.IDLE_LEFT;
            }
        }
    }
    public void moveLeft() //reset the currsprite frame
    {
        //if it's already moving left, animate. If not, restart left walking animation - same for other moves
        if (!InAttack() && !InItem())
        {
            prevPos = currPos;
            prevHitbox = hitbox;
            currPos.X -= (int)PosNums.MOV_RANGE; //updates the position
            //COLLISION SPRINT3
            hitbox.X -= (int)PosNums.MOV_RANGE; //updates the hitbox

            if (!InDamaged())
            {
                if (state == State.MOVE_LEFT) //if alr left, updates animation
                {
                    currSprite.Update();
                }
                else //else gets left walk cycle
                {
                    currSprite = walkSprites[(int)DirNums.LEFT];
                }
            }

            state = State.MOVE_LEFT;
        }
    }

    public void moveRight()
    {
        if (!InAttack() && !InItem())
        {
            prevPos = currPos;
            prevHitbox = hitbox;
            currPos.X += (int)PosNums.MOV_RANGE; //updates the position
            hitbox.X += (int)PosNums.MOV_RANGE; //updates the hitbox
            if (!InDamaged())
            {
                if (state == State.MOVE_RIGHT) //if alr left, updates animation
                {
                    currSprite.Update();
                }
                else //else gets left walk cycle
                {
                    currSprite = walkSprites[(int)DirNums.RIGHT];
                }
            }

            state = State.MOVE_RIGHT;
        }
    }

    public void moveDown()
    {
        if (!InAttack() && !InItem())
        {

            prevPos = currPos;
            prevHitbox = hitbox;
            currPos.Y += (int)PosNums.MOV_RANGE; //updates the position
            hitbox.Y += (int)PosNums.MOV_RANGE; //updates the hitbox
            if (!InDamaged())
            {
                if (state == State.MOVE_DOWN) //if alr left, updates animation
                {
                    currSprite.Update();
                }
                else //else gets left walk cycle
                {
                    currSprite = walkSprites[(int)DirNums.DOWN];
                }
            }

            state = State.MOVE_DOWN;
        }
    }

    public void moveUp()
    {
        if (!InAttack() && !InItem())
        {
            prevPos = currPos;
            prevHitbox = hitbox;
            currPos.Y -= (int)PosNums.MOV_RANGE; //updates the position
            hitbox.Y -= (int)PosNums.MOV_RANGE; //updates the hitbox
            if (!InDamaged())
            {
                if (state == State.MOVE_UP) //if alr left, updates animation
                {
                    currSprite.Update();
                }
                else//else gets left walk cycle
                {
                    currSprite = walkSprites[(int)DirNums.UP];
                }
            }

            state = State.MOVE_UP;
        }
    }

    public void setDamaged()
    {
        if (!InDamaged()) {
            damagedCounter = 1;
        //damaged direction depends on last direction
        if (isDown())
        {
            currSprite = damagedSprites[(int)DirNums.DOWN];
            state = State.DAMAGED_DOWN;
        }
        else if (isUp())
        {
            currSprite = damagedSprites[(int)DirNums.UP];
            state = State.DAMAGED_UP;
        }
        else if (isRight())
        {
            currSprite = damagedSprites[(int)DirNums.RIGHT];
            state = State.DAMAGED_RIGHT;
        }
        else if (isLeft())
        {
            currSprite = damagedSprites[(int)DirNums.LEFT];
            state = State.DAMAGED_LEFT;
        }
        }
    }

    public Vector2 attack()
    {
        Vector2 rangeAttack = currPos; //hitbox range

        if (!InAttack() && !InItem() && !InDamaged())
        {
            //direction sprite and direction of attack hitbox depends on last dir.
            if (state == State.IDLE_DOWN)
            {
                rangeAttack.Y += 1;
                currSprite = attackSprites[(int)DirNums.DOWN];
                state = State.ATTACK_DOWN;
                attackCounter = 1;
            }
            else if (state == State.IDLE_UP)
            {
                rangeAttack.Y -= 1;
                currSprite = attackSprites[(int)DirNums.UP];
                state = State.ATTACK_UP;
                attackCounter = 1;
            }
            else if (state == State.IDLE_LEFT)
            {
                rangeAttack.X -= 1;
                currSprite = attackSprites[(int)DirNums.LEFT];
                state = State.ATTACK_LEFT;
                attackCounter = 1;
            }
            else if (state == State.IDLE_RIGHT)
            {
                rangeAttack.X += 1;
                currSprite = attackSprites[(int)DirNums.RIGHT];
                state = State.ATTACK_RIGHT;
                attackCounter = 1;
            }
        }


        return rangeAttack;
    }

    public void updateAttack()
    {

        //locks player in attack anim.
        if (attackCounter > 0)
        {
            attackCounter++;
            currSprite.Update();
        }
        if(attackCounter > (int)MaxFrames.MAX_ATTACK)
        {
            attackCounter = 0;
            setIdle();
        }

    }

    public void updateItem()
    {
        //locks player in item animation
        if (itemCounter > 0) itemCounter++;
        if (itemCounter > (int)MaxFrames.MAX_ITEM)
        {
            itemCounter = 0;
            setIdle();
        }

    }

    public void updateDamaged()
    {
        if (damagedCounter > 0) damagedCounter++;
        if (damagedCounter > (int)MaxFrames.MAX_DAMAGED)
        {
            damagedCounter = 0;
            setIdle();
        }
    }

    public  IProjectile useItem(string itemName)
    {
        
        if (!InAttack() && !InItem() && !InDamaged())
        {
            //direction of item depends on last dir
            Vector2 dir = new Vector2(0, 0);
            Vector2 shootPos = currPos;
            shootPos.X += (float)PlayerTextureDims.WIDTH / 3;
            shootPos.Y += (float)PlayerTextureDims.HEIGHT / 3;

            if (state == State.IDLE_DOWN)
            {
                currSprite = itemSprites[(int)DirNums.DOWN];
                state = State.ITEM_DOWN;
                itemCounter = 1;
                dir.Y += 1;
                
                return (factory.GetProjectile(itemName, shootPos, game.Content, dir));

            }
            else if (state == State.IDLE_UP)
            {
                currSprite = itemSprites[(int)DirNums.UP];
                state = State.ITEM_UP;
                itemCounter = 1;
                dir.Y -= 1;
                
                return (factory.GetProjectile(itemName, shootPos, game.Content, dir));

            }
            else if (state == State.IDLE_LEFT)
            {
                currSprite = itemSprites[(int)DirNums.LEFT];
                state = State.ITEM_LEFT;
                itemCounter = 1;
                dir.X -= 1;
                
                return (factory.GetProjectile(itemName, shootPos, game.Content, dir));
            }
            else if (state == State.IDLE_RIGHT)
            {
                currSprite = itemSprites[(int)DirNums.RIGHT];
                state = State.ITEM_RIGHT;
                itemCounter = 1;
                dir.X += 1;
                
                return (factory.GetProjectile(itemName, shootPos, game.Content, dir));
            }

        }
        return null;
    }

    public void setLastPos()
    {
        currPos = prevPos;
        hitbox = prevHitbox;
    }

}
