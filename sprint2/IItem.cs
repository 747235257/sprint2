using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public interface IItem
    {
        public void CurrentItemMinus();
        public void CurrentItemPlus();
        public void ItemProcess(SpriteBatch spriteBatch, Vector2 loc);

    }
}
