using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

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

        public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);
            Vector2 range = new Vector2(0, 0);
            if (gstate.IsButtonDown(Buttons.A))
            {
                range = player.attack();
            }
            HandleNoPlayerInput(gstate, player);
            return range;
        }

        public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);
            
            if (gstate.IsButtonDown(Buttons.X))
            {
                player.setDamaged();
            }
            HandleNoPlayerInput(gstate, player);
            
        }

        public IProjectile HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);
            IProjectile proj = null;
            if (gstate.IsButtonDown(Buttons.RightShoulder))
            {
                proj = player.useItem("Nunchucks");
            }
            else if(gstate.IsButtonDown(Buttons.LeftShoulder))
            {
                proj = player.useItem("Nunchucks");
            }
            else if (gstate.IsButtonDown(Buttons.LeftTrigger))
            {
                proj = player.useItem("Nunchucks");
            }

            HandleNoPlayerInput(gstate, player);
            return proj;
        }


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

            HandleNoPlayerInput(gstate, player);

        }

        public void HandleItem(GraphicsDeviceManager _graphics, IItem item)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);
            if (gstate.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                item.CurrentItemPlus();
            }
            else if (gstate.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                item.CurrentItemMinus();
            }
        }

        public void HandleNoPlayerInput(GamePadState gstate, IPlayer player)
        {
            if (gstate.IsButtonDown(Buttons.DPadUp) && gstate.IsButtonDown(Buttons.DPadUp) && gstate.IsButtonDown(Buttons.DPadDown) 
                && gstate.IsButtonDown(Buttons.DPadDown) && gstate.IsButtonDown(Buttons.DPadLeft) && gstate.IsButtonDown(Buttons.DPadLeft)
                && gstate.IsButtonDown(Buttons.DPadRight) && gstate.IsButtonDown(Buttons.DPadRight) && gstate.IsButtonDown(Buttons.A) 
                && gstate.IsButtonDown(Buttons.A) && gstate.IsButtonDown(Buttons.DPadUp) && gstate.IsButtonDown(Buttons.DPadUp) 
                && gstate.IsButtonDown(Buttons.DPadUp) && gstate.IsButtonDown(Buttons.DPadUp))
            {
                player.setIdle();
            }
        }

        public bool HandleSwitchEnemy(int currentNPC)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);
            if (gstate.IsButtonDown(Buttons.LeftStick))
            {
                game.cur.Stop();
                currentNPC = (currentNPC + 1) % 6;//6 is the total types of enemies.
                game.currentNPC = currentNPC;
                return true;
            }
            else if (gstate.IsButtonDown(Buttons.RightStick))
            {
                game.cur.Stop();
                currentNPC = (currentNPC + 5) % 6;

                game.currentNPC = currentNPC;
                return true;
            }
            return false;
        }

        public IBlock blockHandle(GraphicsDeviceManager _graphics, Texture2D block, int spriteRow, int spriteCol, Vector2 initPosition, IBlock blocky)
        {
            var gstate = GamePad.GetState(PlayerIndex.One);

            if (gstate.IsButtonDown(Buttons.RightThumbstickUp))
            {
                //blocky = new Block(block, spriteRow, spriteCol, initPosition);
                blocky.switchBlock(_graphics, Block.FrameDirection.Forward);
            }
            if (gstate.IsButtonDown(Buttons.RightThumbstickDown))
            {
                //blocky = new Block(block, spriteRow, spriteCol, initPosition);
                blocky.switchBlock(_graphics, Block.FrameDirection.Backward);
            }
            return blocky;
        }

    }
}