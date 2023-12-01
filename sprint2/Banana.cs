using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint2
{

    public class Banana : Projectile 
    {
        //constants
        private readonly Vector2 velocity = new Vector2(250, 250); //the projectile will travel 250 pixels per sec
        private readonly int rangeValue = 150;
        private readonly int row = 1;
        private readonly int column = 1;
        

        private enum HitboxDims
        {
            WIDTH = 40, HEIGHT = 35, X_ADJ = 10, Y_ADJ = 15, ROW = 1, COL = 1
        }
        //constructor that accepts current user's position and direction of the projectile
        public Banana(Vector2 position, ContentManager Content, Vector2 initialDirection)
            : base(position, Content, initialDirection) 
        {
            Position = position;  
            Direction = initialDirection;
            currentRange = 0;
            isActive = true;
            Texture = Content.Load<Texture2D>("wep3hud");
            hitboxTexture = Content.Load<Texture2D>("hitbox");
            currSprite = new NonMoveAnimatedSprite(Texture, row, column, Position);
            hitbox = new Rectangle((int)Position.X + (int)HitboxDims.X_ADJ, (int)Position.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            hitboxSprite = new NonMoveAnimatedSprite(hitboxTexture, (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
            Velocity.X = Direction.X * velocity.X; //kinda bad physics logic
            Velocity.Y = Direction.Y * velocity.Y;
            range = rangeValue;

            hit_width = (int)HitboxDims.WIDTH;
            hit_height = (int)HitboxDims.HEIGHT;
            x_adj = (int)HitboxDims.X_ADJ;
            y_adj = (int)HitboxDims.Y_ADJ;
        }
        
    }
}
