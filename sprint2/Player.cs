using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace sprint2
{

    public class Player : IPlayer
    {
        IPlayerStateMachine playerState;
        Game game;
        public Player(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            this.game = game;
            playerState = new PlayerStateMachine(game, graphics, spriteBatch);
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
            //playerState.drawCurrentSprite();
            //playerState.setIdle();

            return range;
        }
        public void setDamaged()
        {
            playerState.setDamaged();
        }

        public void updateAttack()
        {
            playerState.updateAttack();
        }

        public void updateItem()
        {
            playerState.updateItem();
        }
        //returns PROJECTILE used
        public IProjectile useItem(string itemName)
        {
            return playerState.useItem(itemName);
            //playerState.drawCurrentSprite();

        }
        public void Draw()
        {
            playerState.drawCurrentSprite();
        }

    }
}
