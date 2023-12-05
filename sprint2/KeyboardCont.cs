using Microsoft.Xna.Framework.Input;
using sprint2;
using System.Collections.Generic;

namespace sprint2;
public class KeyboardCont : IController
{

    private Game1 game;
    private Dictionary<Keys, ICommand> keyMap = new Dictionary<Keys, ICommand>();


    public KeyboardCont(Game1 game)
    {
        this.game = game;
    }

    public void RegisterCommand(IPlayer player)
    {
        ICommand attack = new AttackCommand(player);
        keyMap[Keys.N] = attack;
        keyMap[Keys.Z] = attack;    
        ICommand movementUp = new MoveUpCommand(player);
        keyMap[Keys.W] = movementUp;
        keyMap[Keys.Up] = movementUp;
        ICommand movementLeft = new MoveLeftCommand(player);
        keyMap[Keys.A] = movementLeft;
        keyMap[Keys.Left] = movementLeft;
        ICommand movementDown = new MoveDownCommand(player);
        keyMap[Keys.S] = movementDown;
        keyMap[Keys.Down] = movementDown;
        ICommand movementRight = new MoveRightCommand(player);
        keyMap[Keys.D] = movementRight;
        keyMap[Keys.Right] = movementRight;
    }

    public void HandleAttack(IPlayer player)
    {
        var kstate = Keyboard.GetState();
               
        if (kstate.IsKeyDown(Keys.N) || kstate.IsKeyDown(Keys.Z))
        {
            keyMap[Keys.Z].Execute(); 
        }
        HandleNoPlayerInput(kstate, player);
     }

    
    public List<IProjectile> HandlePlayerItem(IPlayer player)
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

    public void HandleMovement(IPlayer player)
    {
        KeyboardState kstate = Keyboard.GetState();
        
        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
        {
            keyMap[Keys.W].Execute();
        }
        else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
        {
            keyMap[Keys.A].Execute();
        }
        else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
        {
            keyMap[Keys.S].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
        {
            keyMap[Keys.D].Execute();
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

    public void HandleSwitchInventory(Inventory inventory)
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
