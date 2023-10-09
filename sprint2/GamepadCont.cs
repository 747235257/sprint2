using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace sprint2
{
    public class GamepadCont : IController
    {

        private Game1 game;
        public int index;
        public GamepadCont(Game1 game, int index)
        {
            this.game = game;
            this.index = index;
        }

        //public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player)

        //public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player)

        //return projectile class
        //public IProjectile HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player)


        public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player)
        {
            GamePadState gstate = GamePad.GetState(PlayerIndex.One);
            if (gstate.IsButtonDown(Buttons.DPadUp) || gstate.IsButtonDown(Buttons.DPadUp))
            {
                player.moveUp();
            }
            else if (gstate.IsButtonDown(Buttons.DPadLeft) || gstate.IsButtonDown(Buttons.DPadLeft))
            {
                player.moveLeft();
            }
            else if (gstate.IsButtonDown(Buttons.DPadDown) || gstate.IsButtonDown(Buttons.DPadDown))
            {
                player.moveDown();
            }
            else if (gstate.IsButtonDown(Buttons.DPadRight) || gstate.IsButtonDown(Buttons.DPadRight))
            {
                player.moveRight();
            }

            //HandleNoPlayerInput(gstate, player);

        }

        public void HandleItem(GraphicsDeviceManager _graphics, IItem item)
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.U))
            {
                item.CurrentItemPlus();
            }
            else if (kstate.IsKeyDown(Keys.I))
            {
                item.CurrentItemMinus();
            }
        }

        //public void HandleNoPlayerInput(KeyboardState kstate, IPlayer player)

        //public bool HandleSwitchEnemy(int currentNPC)

        //public IBlock blockHandle(GraphicsDeviceManager _graphics, Texture2D block, int spriteRow, int spriteCol, Vector2 initPosition, IBlock blocky)

    }
}