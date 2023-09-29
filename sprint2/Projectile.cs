using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace sprint2
{
    public class Projectile : IProjectile
    {
        //projectile's fields 
        protected Texture2D Texture; //protected
        protected Vector2 Position, Direction;
        protected Vector2 Velocity;
        protected bool isActive;
        protected int range, currentRange;
        protected ISprite currSprite;

        //base constructor for projectile
        public Projectile(Vector2 position, ContentManager Content, Vector2 initialDirection) // pass game
        {
            Position = position;  // put enemy position here
            Direction = initialDirection;
            currentRange = 0;
            isActive = true;
        }

        
        public void UpdatePosition(GameTime gameTime)
        {
            //only updates position if the current projectile is active
            if (isActive)
            {
                Position.X += Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.Y += Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentRange++;
                currSprite.Update();
                CheckRange();

            }
        }

        
        //draw only if current projectile is active
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {


                currSprite.Draw(spriteBatch, Position);


            }
        }

        //check the current range of projectile if exceeds the maximum range
        public void CheckRange()
        {
            if (currentRange > range)
            {
                isActive = false;
                //DeadAnimation();
            }
        }

        public bool ReturnStatus()
        {
            return isActive;
        }

    }
}


