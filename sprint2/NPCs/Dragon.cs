using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class Dragon:INPC
    {
        public INPCSprite DragonSprite{get; set;}
        private Texture2D texture { get; set;}
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private float duration;
        private bool attack;
        private Rectangle destination;
        private Game1 game;
        public Dragon(Texture2D texture, SpriteBatch spriteBatch, Game1 game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            DragonSprite = new DragonSprite(this.texture, this.spriteBatch);
            count = 0;
            duration = 0;
            attack = false;
            this.game = game;
        }
        
        

        

        public List<IProjectile> Attack()
        {
            //The dragon gonna fire 3 fireballs.
            List<IProjectile> projectiles = new List<IProjectile>();
            projectiles.Add(new Projectile(new Vector2(destination.X, destination.Y), "Dragon", game.Content, new Vector2(-1, 1)));
            projectiles.Add(new Projectile(new Vector2(destination.X, destination.Y), "Dragon", game.Content, new Vector2(-1, 0)));
            projectiles.Add(new Projectile(new Vector2(destination.X, destination.Y), "Dragon", game.Content, new Vector2(-1, -1)));

            return projectiles;



        }
        public void Stop()
        {
            //Reset the npc.
            count = 0;
            duration = 0;
            DragonSprite = new DragonSprite(texture, spriteBatch);
        }

        public List<IProjectile> Execute(GameTime gametime)
        {

            List<IProjectile> projectiles = null;
            count++;
            if (attack)
            {   
                if (duration == 0)
                {
                    projectiles = Attack();
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                }
                else if (duration > 2)//The cool down duration for an attack is 2 seconds.
                {
                    duration = 0;
                    attack = false;
                }else
                {
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                    return null;
                }
                
            }
            if (count % 16 == 0)//Interval of a diraction generator.
            {
                curdir = rnd.Next(0, 3);
                if(curdir == 0)
                {
                    attack= true;
                }
                
            } 
            
            destination = DragonSprite.Update(gametime, curdir);
            count = count % 16;//Reset the count to prevent unnecessary storage usage.
            return projectiles;
            
            
        }
        public void Draw()
        {
            DragonSprite.Draw();
        }
        
    }
}
