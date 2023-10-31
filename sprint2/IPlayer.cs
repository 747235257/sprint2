﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace sprint2
{
    public interface IPlayer
    {

        public void setIdle();
        public void setLocation(Vector2 pos);
        public void moveLeft();


        public void moveRight();


        public void moveDown();


        public void moveUp();

        public void updatePlayer();
        public void setDamaged();
        public Rectangle getHitbox();

        public void setLastPos();

        public Vector2 attack(); //returns attack hitbox range

        public List<IProjectile> useItem(string itemName); //returns the projectile shot
        public void Draw();

        public bool isAlive();

        public void reduceHP();
        public int getHealth();
        public List<string> getInventory();
        public void pickUpItem(string itemName);
    }
}
