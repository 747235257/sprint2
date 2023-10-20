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
        
        

        public List<IProjectile> Attack();
        public void Stop();

        public List<IProjectile> Execute(GameTime gametime);
        public void Draw();

        public void drawHitbox();

        public Rectangle getHitbox();

        public void giveDamage();

        public bool isStillAlive();

        public void setLastPos();


    }
}
