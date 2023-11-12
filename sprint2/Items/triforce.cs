using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace sprint2
{
    public class triforce : IItem
    {

        private ISprite itemSprite;
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Vector2 pos;
        private ISprite hitboxSprite;
        private Rectangle hitbox;
        private bool isActive;
        private string name;

        private enum HitboxDims
        {
            WIDTH = 20, HEIGHT = 20, X_ADJ = 20, Y_ADJ = 20, ROW = 1, COL = 1
        }

        public triforce(Vector2 pos, Game1 game, SpriteBatch spriteBatch)
        {
            this.game = game;
            itemSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("triforce"), 1, 1, pos);
            this.spriteBatch = spriteBatch;
            this.pos = pos;
            this.isActive = true;
            this.name = "triforce";
            //hitbox set
            hitbox = new Rectangle((int)pos.X + (int)HitboxDims.X_ADJ, (int)pos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            //hitbox assets
            hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));
        }

        public void setInactive()
        {
            this.isActive = false;
        }

        public bool isAlive()
        {
            return isActive;
        }

        public string getItemName()
        {
            return name;
        }
        public void Draw()
        {
            DrawHitbox();
            itemSprite.Draw(spriteBatch, pos);
        }

        public Rectangle getHitbox()
        {
            return hitbox;
        }
        public void DrawHitbox()
        {
            hitboxSprite.DrawHitbox(spriteBatch, new Vector2(hitbox.X, hitbox.Y), hitbox);
        }


    }
}
