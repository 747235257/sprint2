using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace sprint2
{
    public class Player : IPlayer
    {
        private Vector2 currPos;
        private ISprite currWalk;
        List<ISprite> walkSprites;
        int rows = 1;
        int cols = 10;

        public  Player(Vector2 startPos)
        {
            currPos = startPos;
            walkSprites = new List<ISprite>();
        }

        public void LoadSprite(Texture2D TrightWalk, Texture2D TleftWalk, Texture2D TupWalk, Texture2D TdownWalk, Texture2D TinitialStand)
        {
            walkSprites.Add(new NonMoveAnimatedSprite(TrightWalk, rows, cols, currPos));
            walkSprites.Add(new NonMoveAnimatedSprite(TleftWalk, rows, cols, currPos));
            walkSprites.Add(new NonMoveAnimatedSprite(TdownWalk, rows, cols, currPos));
            walkSprites.Add(new NonMoveAnimatedSprite(TupWalk, rows, cols, currPos));

            currWalk = walkSprites[0];
        }

        public void move(Vector2 adj, GraphicsDeviceManager graphics)
        {
            currPos.X += adj.X;
            currPos.Y += adj.Y;
            currWalk.Update(graphics);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currWalk.Draw(spriteBatch, currPos);
        } 

        public void setDirection(int dirCode)
        {
            currWalk = walkSprites[dirCode];
        }
    }
}
