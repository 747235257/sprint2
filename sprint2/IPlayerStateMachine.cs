using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint2
{
    public interface IPlayerStateMachine
    {

        public bool InAttack();
        public Rectangle getAttackHitbox();
        public void drawCurrentSprite();
        public void setIdle();
        public void setLocation(Vector2 pos);
        public void moveLeft();


        public void moveRight();


        public void moveDown();


        public void moveUp();

        public int updateAttack(); //To lock player in attack anim.

        public int updateItem();//To lock player in item anim.

        public int updateDamaged();

        public int retDamagedCount();
        public void setDamaged();

        public void setLastPos();

        public Rectangle getHitbox();

        public Vector2 attack(); //returns attack hitbox range

        public List<IProjectile> useItem(string itemName); //returns the projectile shot

        public Vector2 getPosition();//return current postion of the player
       
    }
}
