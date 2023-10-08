using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace sprint2
{
    public interface IPlayer
    {

        public void setIdle();
        public void moveLeft();


        public void moveRight();


        public void moveDown();


        public void moveUp();

        public void updateAttack(); //To lock player in attack anim.

        public void updateItem();//To lock player in item anim.
        public void setDamaged();
        public Rectangle getHitbox();

        public Vector2 attack(); //returns attack hitbox range

        public IProjectile useItem(string itemName); //returns the projectile shot
        public void Draw();
    }
}
