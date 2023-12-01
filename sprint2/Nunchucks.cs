using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint2
{

    public class Nunchucks : Projectile
    {

        private readonly Vector2 velocity = new Vector2(400, 400); //the projectile will travel 250 pixels per sec
        private readonly int rangevalue = 50;
        private readonly int row = 1;
        private readonly int column = 1;

        public enum HitboxDims
        {
            WIDTH = 20, HEIGHT = 20, X_ADJ = 20, Y_ADJ = 20, ROW = 1, COL = 1
        }

        //constructor that accepts current user's position and direction of the projectile
        public Nunchucks(Vector2 position, ContentManager Content, Vector2 initialDirection)
            : base(position, Content, initialDirection) // pass game
        {
            Position = position;  // put enemy position here
            currentRange = 0;
            isActive = true;
            Texture = Content.Load<Texture2D>("wep1hud");
            hitboxTexture = Content.Load<Texture2D>("hitbox");
            hitbox = new Rectangle((int)Position.X + (int)HitboxDims.X_ADJ, (int)Position.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            currSprite = new NonMoveAnimatedSprite(Texture, row, column, Position);
            hitboxSprite = new NonMoveAnimatedSprite(hitboxTexture, (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
            Velocity.X = Direction.X * velocity.X;
            Velocity.Y = Direction.Y * velocity.Y;
            range = rangevalue;

            hit_width = (int)HitboxDims.WIDTH;
            hit_height = (int)HitboxDims.HEIGHT;
            x_adj = (int)HitboxDims.X_ADJ;
            y_adj = (int)HitboxDims.Y_ADJ;

        }
    }
}
