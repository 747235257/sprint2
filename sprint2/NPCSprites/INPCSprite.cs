using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public interface INPCSprite
    {
        public Rectangle Update(GameTime gametime,int curdir);

        public void Draw();
    }
}
