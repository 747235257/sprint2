using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace sprint2
{

    public class Player : IPlayer
    {
        public IPlayerStateMachine playerState;
        Game game;
        public Player(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 startPos)
        {
            this.game = game;
            playerState = new PlayerStateMachine(game, graphics, spriteBatch, startPos);
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
            playerState.setDamaged();
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
        public IProjectile useItem(string itemName)
        {
            return playerState.useItem(itemName);

        }

        
        public void Draw()
        {
            playerState.drawCurrentSprite();
        }

    }
}
