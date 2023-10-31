using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace sprint2
{
     public abstract class ProjectileFactory
        {
            public abstract List<IProjectile> GetProjectile(string name, Vector2 currPos, ContentManager content, Vector2 dir);
           
        }
    public class ProjectileCreator : ProjectileFactory
    {
        public override List<IProjectile> GetProjectile(string name, Vector2 currPos, ContentManager content, Vector2 dir )
        {

            List<IProjectile> projs = new List<IProjectile>();
            if (name.Equals("Nunchucks"))
            {
               projs.Add(new Nunchucks(currPos, content, dir));
            }

            if (name.Equals("Dragon"))
            {
                projs.Add(new DragonProjectile(currPos, content, dir));
              
                //case if dir = (1, 0) RIGHT
                if(dir.X == 1 && dir.Y == 0)
                {
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(1, 1)));
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(1, -1)));
                }

                //case if dir = (-1, 0) LEFT
                if (dir.X == -1 && dir.Y == 0)
                {
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(-1, 1)));
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(-1, -1)));
                }

                //case if dir = (0, 1) UP
                if (dir.X == 0 && dir.Y == 1)
                {
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(1, 1)));
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(-1, 1)));
                }

                //case if dir = (0, -1) Down
                if (dir.X == 0 && dir.Y == -1)
                {
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(1, -1)));
                    projs.Add(new DragonProjectile(currPos, content, new Vector2(-1, -1)));
                }
            }

            if (name.Equals("Goriya"))
            {
                projs.Add(new Banana(currPos, content, dir));
            }


            return projs;
        }


    }
}
