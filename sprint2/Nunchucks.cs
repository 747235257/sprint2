using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint2
{

    public class Nunchucks : Projectile
    {

        private readonly float time = 250;
        private readonly int rangevalue = 50;
        private readonly int row = 1;
        private readonly int column = 6;

        //constructor that accepts current user's position and direction of the projectile
        public Nunchucks(Vector2 position, ContentManager Content, Vector2 initialDirection)
            : base(position, Content, initialDirection) // pass game
        {
            Position = position;  // put enemy position here
            Direction = initialDirection;
            currentRange = 0;
            isActive = true;
            Texture = Content.Load<Texture2D>("Nunchuks");
            currSprite = new NonMoveAnimatedSprite(Texture, row, column, Position);
            Velocity.X = Direction.X * time;
            Velocity.Y = Direction.Y * time;
            range = rangevalue;
        }
    }
}
