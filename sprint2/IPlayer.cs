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

        public void updateAttack();

        public void updateItem();
        public void setDamaged();

        public Vector2 attack();

        public IProjectile useItem(string itemName);
        public void Draw();
    }
}
