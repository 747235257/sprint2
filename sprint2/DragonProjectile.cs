using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint2
{

    public class DragonProjectile : Projectile
    {
        //constants
        private readonly float time = 250;
        private readonly int rangeValue = 100;
        private readonly int row = 1;
        private readonly int column = 4;

        //constructor that accepts current user's position and direction of the projectile
        public DragonProjectile(Vector2 position, ContentManager Content, Vector2 initialDirection)
            : base(position, Content, initialDirection)
        {
            Position = position;  
            Direction = initialDirection;
            currentRange = 0;
            isActive = true;
            Texture = Content.Load<Texture2D>("projectile");
            currSprite = new NonMoveAnimatedSprite(Texture, row, column, Position);
            Velocity.X = Direction.X * time;
            Velocity.Y = Direction.Y * time;
            range = rangeValue;
        }

    }
}
