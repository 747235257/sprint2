using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace sprint2
{
    public interface IProjectile
    {
        public void UpdatePosition(GameTime gameTime);

        public void LoadProjectile(ContentManager content, string name);

        public void CheckRange();

        public void Draw(SpriteBatch spriteBatch);



        public bool ReturnStatus();



    }
}
