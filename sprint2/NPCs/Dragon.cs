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
        private Vector2 currPos;
        private Vector2 prevPos;
        private Rectangle hitbox;      //COLLISION SPRINT 3
        private Rectangle prevHitbox;
        private ISprite hitboxSprite;
        private Game1 game;
        private ProjectileFactory factory = new ProjectileCreator();
        private readonly Vector2 DIAGONAL_DOWNLEFT = new Vector2(-1, 1), LEFT = new Vector2(-1, 0), DIAGONAL_UPLEFT = new Vector2(-1, -1);
        private string Name;
        private bool isAlive;

        private enum HitboxDims
        {
            WIDTH = 50, HEIGHT = 65, X_ADJ = 0, Y_ADJ = 0, ROW = 1, COL = 1
        }
        public Dragon(Texture2D texture, SpriteBatch spriteBatch, Game1 game, Vector2 startPos)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            DragonSprite = new DragonSprite(this.texture, this.spriteBatch);
            count = 0;
            duration = 0;
            attack = false;
            this.game = game;
            this.Name = "Dragon";
            isAlive = true;

            //gets position of the dragon
            currPos = startPos;
            prevPos = currPos;

            //hitbox allocations
            hitbox = new Rectangle((int)currPos.X + (int)HitboxDims.X_ADJ, (int)currPos.Y + (int)HitboxDims.Y_ADJ, (int)HitboxDims.WIDTH, (int)HitboxDims.HEIGHT);
            prevHitbox = hitbox;

            //hitboxsprite
            hitboxSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hitbox"), (int)HitboxDims.ROW, (int)HitboxDims.COL, new Vector2(hitbox.X, hitbox.Y));


        }





        public List<IProjectile> Attack()
        {

            return factory.GetProjectile(Name, new Vector2(currPos.X, currPos.Y), game.Content, LEFT);



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

            //UPDATES positions and hitboxes
            prevPos = currPos;

            Vector2 updateMove = DragonSprite.Update(gametime, curdir);
            currPos.X += updateMove.X;
            currPos.Y += updateMove.Y;
            count = count % 16;//Reset the count to prevent unnecessary storage usage.

            prevHitbox = hitbox;
            hitbox.X = (int)currPos.X + (int)HitboxDims.X_ADJ;
            hitbox.Y = (int)currPos.Y + (int)HitboxDims.Y_ADJ;

            return projectiles;
            
            
        }
        public void Draw()
        {
            drawHitbox();
            DragonSprite.Draw(currPos);

        }

        //COLLISION SPRINT3
        public void drawHitbox()
        {
            hitboxSprite.DrawHitbox(spriteBatch, new Vector2(hitbox.X, hitbox.Y), hitbox);
        }

        //COLLISION SPRINT3
        public Rectangle getHitbox()
        {
            return hitbox;
        }

        public void giveDamage()
        {
            isAlive = false;
        }

        public bool isStillAlive()
        {
            return isAlive;
        }

        public void setLastPos()
        {
            currPos = prevPos;
            hitbox = prevHitbox;
        }

    }
}
