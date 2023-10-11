using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sprint2.Nunchucks;

namespace sprint2
{
    public class Walls
    {
        Rectangle hitbox {  get; set; }
        Vector2 pos { get; set; }
        ISprite hitboxSprite { get; set; }
        Texture2D texture { get; set; }
        int height { get; set; }
        int width { get; set; }
        private const int ONE = 1;
        public Walls(Game game, Texture2D texture, Vector2 pos, int height, int width)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(pos.X, pos.Y));
            this.pos = pos;
            this.texture = texture;
            this.height = height;
            this.width = width;
        }
        public void drawHitbox(SpriteBatch spriteBatch, Vector2 pos)
        {

            Rectangle sourceRectangle = new Rectangle(width, height, width, height);
            Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, width, height);


            //draws a portion of the texture into a portion of the screen
            hitboxSprite.DrawHitbox(spriteBatch, pos, hitbox);
        }
    }
}
