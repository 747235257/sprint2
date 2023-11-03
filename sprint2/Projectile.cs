using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography;


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
        protected ISprite hitboxSprite;
        protected Rectangle hitbox;
        protected Texture2D hitboxTexture;
        protected int hit_width;
        protected int hit_height;
        protected int x_adj;
        protected int y_adj;


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
                Position.X += (int)(Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds);
                Position.Y += (int)(Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
                hitbox.X = (int)Position.X + x_adj;
                hitbox.Y = (int)Position.Y + y_adj;
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
                //drawHitbox(spriteBatch,new Vector2(hitbox.X, hitbox.Y), hitbox);
                currSprite.Draw(spriteBatch, Position);
            }
        }

        public void drawHitbox(SpriteBatch spriteBatch, Vector2 loc, Rectangle hitbox)
        {
            hitboxSprite.DrawHitbox(spriteBatch, loc, hitbox);
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

        public void setToInactive()
        {
            isActive = false;
        }
        public bool ReturnStatus()
        {
            return isActive;
        }

        public Rectangle getHitbox()
        { 
            return hitbox; 
        }

    }
}


