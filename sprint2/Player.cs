using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace sprint2
{

    public class Player : IPlayer
    {
        private const int startHP = 4;
        public IPlayerStateMachine playerState;
        public int hp;
        private int damageCount;
        Game game;
        private List<string> items;


        public Player(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 startPos)
        {
            this.game = game;
            playerState = new PlayerStateMachine(game, graphics, spriteBatch, startPos);
            hp = startHP;
            damageCount = 0;
            items = new List<string>();
        }

        public void setLocation(Vector2 pos)
        {
            playerState.setLocation(pos);
        }
        public void setIdle()
        {
            playerState.setIdle();
        }
        public void moveLeft()
        {
            playerState.moveLeft();
        }
        public void moveRight()
        {
            playerState.moveRight();
        }
        public void moveUp()
        {
            playerState.moveUp();
        }
        public void moveDown()
        {
            playerState.moveDown();
        }

        public Vector2 attack()
        {
            Vector2 range = playerState.attack();
            return range;
        }

        public Rectangle getHitbox()
        {
            return playerState.getHitbox();
        }

        public void setDamaged()
        {
            reduceHP();
            playerState.setDamaged();
        }
        public void reduceHP()
        {
            //only reduce hp when grace period is over in player state
            if (playerState.retDamagedCount() == 0)
            {
                hp--;
            }

        }
        public int getHealth()
        {
            return hp;
        }

        public List<string> getInventory()
        {
            return items;
        }

        public bool isAlive()
        {
            return hp != 0;
        }

        public void updatePlayer()
        {
            playerState.updateAttack();
            playerState.updateItem();
            playerState.updateDamaged();
        }

        public void setLastPos()
        {
            playerState.setLastPos();
        }
        //returns PROJECTILE used
        public List<IProjectile> useItem(string itemName)
        {
            List<IProjectile> projs = new List<IProjectile>();
            //only uses item is the item is in player inventory
            if (items.Contains(itemName))
            {
                projs.AddRange(playerState.useItem(itemName));
            }

            return projs;
        }
        
        public void pickUpItem(string itemName)
        {
            //only adds item if not in the list.
            if (!items.Contains(itemName))
            {
                items.Add(itemName);
            }
        }
        public void Draw()
        {
            playerState.drawCurrentSprite();
        }

        public Vector2 getPosition()
        {
            return playerState.getPosition();
        }

    }
}
