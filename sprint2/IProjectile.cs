using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace sprint2
{
    public interface IProjectile
    {
        void UpdatePosition(GameTime gameTime);

        void LoadProjectile(ContentManager content, string name);

        void CheckRange();

        void Draw(SpriteBatch spriteBatch);



        bool ReturnStatus();



    }
}
