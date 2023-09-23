using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;


namespace sprint2
{
    public class Projectile : IProjectile
    {

        private Texture2D Texture;
        private Vector2 Position, Direction;
        private Vector2 Velocity;
        private bool isActive;
        private int range, currentRange;
        private ISprite currSprite;

        public Projectile(Vector2 position, string name, ContentManager Content, Vector2 initialDirection) // pass game
        {
            Position = position;  // put enemy position here
            Direction = initialDirection;
            currentRange = 0;
            isActive = true;
            LoadProjectile(Content, name);
        }

        public void LoadProjectile(ContentManager Content, string name)
        {


            if (name == "Nunchuks")
            {
                Texture = Content.Load<Texture2D>("Nunchuks");
                currSprite = new NonMoveAnimatedSprite(Texture, 1, 4, Position);
                range = 50;
                Velocity.X = Direction.X * 250;
                Velocity.Y = Direction.Y * 250;

            }

            else if (name == "Dragon")
            {
                Texture = Content.Load<Texture2D>("projectile");
                currSprite = new NonMoveAnimatedSprite(Texture, 1, 4, Position);
                range = 100;
                Velocity.X = Direction.X * 250;
                Velocity.Y = Direction.Y * 250;

            }

            else if (name == "Goriya")
            {
                Texture = Content.Load<Texture2D>("Banana");

                currSprite = new NonMoveAnimatedSprite(Texture, 1, 4, Position);
                range = 150;
                Velocity.X = Direction.X * 250;
                Velocity.Y = Direction.Y * 250;

            }


            }
        

        public void UpdatePosition(GameTime gameTime)
            {
            if (isActive)
                {
                    Position.X += Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Position.Y += Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    currentRange++;
                    currSprite.Update();

                    CheckRange();

                }
            }
            public void Draw(SpriteBatch spriteBatch)
            {
                if (isActive)
                {


                    currSprite.Draw(spriteBatch, Position);


                }
            }

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


