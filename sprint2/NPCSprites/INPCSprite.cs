﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public interface INPCSprite
    {
        public Vector2 Update(GameTime gametime,int curdir);
        public Vector2 GetPos();
        public void Draw(Vector2 pos);
    }
}
