using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace sprint2
{
    public interface IPlayer
    {
        public void LoadSprite(Texture2D TrightWalk, Texture2D TleftWalk, Texture2D TupWalk, Texture2D TdownWalk, Texture2D TinitialStand);
        public void move(Vector2 adj, GraphicsDeviceManager graphics);

        public void Draw(SpriteBatch spriteBatch);
        public void setDirection(int dirCode);
    }
}
