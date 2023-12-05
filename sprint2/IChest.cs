using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public interface IChest
    {
        public void openChest(IChest chest, SpriteBatch spriteBatch);

        public void drawRandomChest();

        public Boolean isOpen();
        public Boolean isTouching();
        public Rectangle getHitbox();
    }
}
