﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;
using System;
using System.Collections.Generic;

namespace sprint2;
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
    public List<IProjectile> HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();
        List<IProjectile> projs = new List<IProjectile>();
        if (kstate.IsKeyDown(Keys.D1))
        {
            projs.AddRange(player.useItem(0));
        }
        else if(kstate.IsKeyDown(Keys.D2))
        {
            projs.AddRange(player.useItem(1));
        } 
        else if (kstate.IsKeyDown(Keys.D3))
        {
            projs.AddRange(player.useItem(2));
        }

        HandleNoPlayerInput(kstate, player);
        return projs;
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
            game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
            game.obstacleHandler.Update();
            game.WallHitboxHandler();
        } else if(mouseState.RightButton == ButtonState.Pressed)
        {
            game.curLevel = game.levelManager.Levels[8];
            game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests );
            game.obstacleHandler.Update();

            game.randomLevelHandler = new RandomLevelHandler(game, game.blocks);
            game.randomLevelHandler.Update();
            game.WallHitboxHandler();
        }
    }

    public void HandleSwitchInventory(IPlayer player, Inventory inventory)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.D1))
        {
            inventory.handleSwitchInventory(1);
        }
        else if (kstate.IsKeyDown(Keys.D2))
        {
            inventory.handleSwitchInventory(2);
        }
        else if (kstate.IsKeyDown(Keys.D3))
        {
            inventory.handleSwitchInventory(3);
        }
        else if (kstate.IsKeyDown(Keys.D4))
        {
            inventory.handleSwitchInventory(4);
        }
        else if (kstate.IsKeyDown(Keys.D5))
        {
            inventory.handleSwitchInventory(5);
        }
        else if (kstate.IsKeyDown(Keys.D6))
        {
            inventory.handleSwitchInventory(6);
        }
        else if (kstate.IsKeyDown(Keys.D7))
        {
            inventory.handleSwitchInventory(7);
        }
        else if (kstate.IsKeyDown(Keys.D8))
        {
            inventory.handleSwitchInventory(8);
        }
        else if (kstate.IsKeyDown(Keys.D9))
        {
            inventory.handleSwitchInventory(9);
        }

    }

    public void HandlePause(Game1 game)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Tab))
        {
            game.pauseGame();
        }
    }

}
