using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    internal class Ring
    {
        private Texture2D tex;
        private int width, height;
        private Vector2 position;
        private IPlayer player;
        private Game1 game;

        public Ring(Texture2D tex, Game1 _game) {
            this.tex = tex;
            this.game = _game;
            player = this.game.player;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
        public void Update()
        {

        }
        private Vector2 Circle(Vector2 position)
        {
            return position;
        }
    }
}
