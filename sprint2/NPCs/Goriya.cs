﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace sprint2
{
    public class Goriya:INPC
    {
        public INPCSprite GoriyaSprite { get; set; }
        private Texture2D texture { get; set; }
        private SpriteBatch spriteBatch;
        private int count;
        private Random rnd = new Random();
        private int curdir;
        private int attackDir;
        private float duration;
        private bool attack;
        private Rectangle destination;
        private Game1 game;
        private string Name;
        private ProjectileFactory factory = new ProjectileCreator();

        public Goriya(Texture2D texture, SpriteBatch spriteBatch, Game1 game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.Name = "Goriya";
            GoriyaSprite = new GoriyaSprite(this.texture, this.spriteBatch);
            count = 0;
            duration = 0;
            attack = false;
            this.game = game;
        }





        public List<IProjectile> Attack()
        {
            List<IProjectile> projectiles = new List<IProjectile>();
            Vector2 dir = new Vector2();
            switch (attackDir)
            {
                case 1:
                    dir = new Vector2(0,-1);
                    break;
                case 2:
                    dir = new Vector2(0,1);
                    break ;
                case 3:
                    dir = new Vector2(-1,0);
                    break;
                case 4:
                    dir = new Vector2(1,0);
                    break;
                default:
                    break;

            }
            
            projectiles.Add(factory.GetProjectile(Name, new Vector2(destination.X, destination.Y), game.Content, dir));
            return projectiles;

        }
        public void Stop()
        {
            //Reset the npc.
            count = 0;
            duration = 0;
            GoriyaSprite = new GoriyaSprite(texture, spriteBatch);
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
                }
                else
                {
                    duration += (float)gametime.ElapsedGameTime.TotalSeconds;
                    return projectiles;
                }
            }
            if (count % 16 == 0)//Interval of a diraction generator.
            {
                curdir = rnd.Next(0, 5);
                if (curdir == 0)
                {
                    
                    attack = true;
                }
                else
                {
                    attackDir = curdir;
                }

            }

            destination = GoriyaSprite.Update(gametime, curdir);
            count = count % 16;//Reset the count to prevent unnecessary storage usage.
            return projectiles;


        }
        public void Draw()
        {
            GoriyaSprite.Draw();
        }
    }
}
