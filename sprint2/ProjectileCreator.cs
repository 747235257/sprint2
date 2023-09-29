using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace sprint2
{
     public abstract class ProjectileFactory
        {
            public abstract IProjectile GetProjectile(string name, Vector2 currPos, ContentManager content, Vector2 dir);
           
        }
    public class ProjectileCreator : ProjectileFactory
    {
        public override IProjectile GetProjectile(string name, Vector2 currPos, ContentManager content, Vector2 dir )
        {
            //switch case for creating a specific projectile instance based on the name
            switch (name)
            {
                case "Nunchucks": return new Nunchucks(currPos, content, dir);
                case "Goriya": return new Banana(currPos, content, dir);
                case "Dragon": return new DragonProjectile(currPos, content, dir);
                default: return null;
            }
        }


    }
}
