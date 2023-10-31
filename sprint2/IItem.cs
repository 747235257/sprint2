using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sprint2
{
    public interface IItem
    {
        public void setInactive();

        public void Draw();

        public void DrawHitbox();

        public string getItemName();
        public Rectangle getHitbox();
        public bool isAlive();

    }
}
