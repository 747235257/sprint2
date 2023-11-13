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
        public int keyCount;
        public bool hasTriforce;
        private int damageCount;
        Game game;
        private List<string> items;
        private HashSet<string> inventory;

        private enum InventoryConst
        {
            MAX_INV = 3
        }

        public Player(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 startPos)
        {
            this.game = game;
            playerState = new PlayerStateMachine(game, graphics, spriteBatch, startPos);
            hp = startHP;
            damageCount = 0;
            items = new List<string>();
            inventory = new HashSet<string>();
            keyCount = 0;
            hasTriforce = false;
        }

        public bool getHasWon()
        {
            return hasTriforce;
        }
        public void incrementKeyCount()
        {
            keyCount++;
        }

        public void decrementKeyCount()
        {
            keyCount--;
        }

        public int getKeyCount()
        {
            return keyCount;
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
                if (hp != 0) hp--;
            }

        }
        public int getHealth()
        {
            return hp;
        }

        public List<string> getItemSlot()
        {
            return items;
        }

        public HashSet<string> getInventory()
        {
            return inventory;
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
        public List<IProjectile> useItem(int keyNum)
        {
            List<IProjectile> projs = new List<IProjectile>();

            //only uses item if in the item bar
            if (items.Count > keyNum)
            {
                string itemName = items[keyNum];
                projs.AddRange(playerState.useItem(itemName));
            }

            return projs;
        }
        
        public void pickUpItem(string itemName)
        {
            //if triforce object is obtained
            if (itemName.Equals("triforce"))
            {
                hasTriforce = true;
            }
            else if (itemName.Equals("key")) //if a key is picked up
            {
                incrementKeyCount();
            }
            else if (!inventory.Contains(itemName)) //else, the weapon items
            {
                inventory.Add(itemName);
                reshuffleItems(itemName);
            }
        }

        public void setItems(int i, string itemName)
        {
            items[i] = itemName;
        }
        //fills up empty spots in the item bar
        private void reshuffleItems(string itemName)
        {
            if(items.Count < (int)InventoryConst.MAX_INV)
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
