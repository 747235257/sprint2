using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace sprint2
{
    public interface IProjectile
    {
        public void UpdatePosition(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch);

        public void CheckRange();

        public bool ReturnStatus();

        public void drawHitbox(SpriteBatch spriteBatch, Vector2 loc, Rectangle hitbox);

        public Rectangle getHitbox();



    }
}
