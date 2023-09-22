using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public interface INPC
    {
        
        

        public void Attack();
        public void Stop();

        public void Execute(GameTime gametime);
        public void Draw();
    }
}
