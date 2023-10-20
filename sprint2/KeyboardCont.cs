﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;
using System;


public class KeyboardCont : IController
{

    private Game1 game;
    public KeyboardCont(Game1 game)
    {
        this.game = game;
    }

    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();
        Vector2 range = new Vector2(0, 0);
        if (kstate.IsKeyDown(Keys.N) || kstate.IsKeyDown(Keys.Z))
        {
            range = player.attack();
        }
        HandleNoPlayerInput(kstate, player);
        return range;
    }

    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.E))
        {
            player.setDamaged();
        }
        HandleNoPlayerInput(kstate, player);
    }
    //return projectile class
    public IProjectile HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();
        IProjectile proj = null;
        if (kstate.IsKeyDown(Keys.D1))
        {
            proj = player.useItem("Nunchucks");
        }
        else if(kstate.IsKeyDown(Keys.D2))
        {
            proj = player.useItem("Goriya");
        } 
        else if (kstate.IsKeyDown(Keys.D3))
        {
            proj = player.useItem("Dragon");
        }

        HandleNoPlayerInput(kstate, player);
        return proj;
    }

    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player)
    {
        KeyboardState kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
        {
            player.moveUp();
        }
        else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
        {
            player.moveLeft();
        }
        else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
        {
            player.moveDown();
        }
        else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
        {
            player.moveRight();
        }

        HandleNoPlayerInput(kstate, player);

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

    public void HandleNoPlayerInput(KeyboardState kstate, IPlayer player)
    {
        if (kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.A) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.S)
            && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.N) && kstate.IsKeyUp(Keys.Z)
            && kstate.IsKeyUp(Keys.E) && kstate.IsKeyUp(Keys.D1) && kstate.IsKeyUp(Keys.D2) && kstate.IsKeyUp(Keys.D3))
        {
            player.setIdle();
        }
    }

    public bool HandleSwitchEnemy(int currentNPC)
    {
        KeyboardState kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.O))
        {
            game.cur.Stop();
            currentNPC = (currentNPC + 1) % 6;//6 is the total types of enemies.
            game.currentNPC = currentNPC;
            return true;
        }
        else if (kstate.IsKeyDown(Keys.P))
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
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Y))
        {
            //blocky = new Block(block, spriteRow, spriteCol, initPosition);
            blocky.switchBlock(_graphics, Block.FrameDirection.Forward);
        }
        if (kstate.IsKeyDown(Keys.T))
        {
            //blocky = new Block(block, spriteRow, spriteCol, initPosition);
            blocky.switchBlock(_graphics, Block.FrameDirection.Backward);
        }
        return blocky;
    }
    public void handleLevelSwitch(Game1 game)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            game.curLevel = game.levelManager.Levels[0];
            game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks);
            game.obstacleHandler.Update();
            game.WallHitboxHandler();
        } else if(mouseState.RightButton == ButtonState.Pressed)
        {
            game.curLevel = game.levelManager.Levels[1];
            game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks);
            game.obstacleHandler.Update();
            game.WallHitboxHandler();
        }
    }
}
