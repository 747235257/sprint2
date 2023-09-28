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

        public void drawCurrentSprite();
        public void setIdle();
        public void moveLeft();


        public void moveRight();


        public void moveDown();


        public void moveUp();

        public void updateAttack(); //To lock player in attack anim.

        public void updateItem();//To lock player in item anim.
        public void setDamaged();

        public Vector2 attack(); //returns attack hitbox range

        public IProjectile useItem(string itemName); //returns the projectile shot
        public bool InAttack();
        public bool InItem();





    }
}
