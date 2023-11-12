using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace sprint2
{
    public interface IPlayer
    {

        public bool getHasWon();
        public void incrementKeyCount();
            
        public int getKeyCount();
        public void decrementKeyCount();
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

        public List<IProjectile> useItem(int keyNum); //returns the projectile shot
        public void Draw();

        public bool isAlive();

        public void reduceHP();
        public int getHealth();
        public HashSet<string> getInventory();

        public List<string> getItemSlot();
        public void pickUpItem(string itemName);

        public Vector2 getPosition();//return current postion of the player
    }
}
