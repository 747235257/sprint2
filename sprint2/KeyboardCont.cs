using Microsoft.Xna.Framework.Input;
using sprint2;
using System.Collections.Generic;

namespace sprint2;
public class KeyboardCont : IController
{

    private Game1 game;
    private IPlayer player;
    private Inventory inventory;
    private MusicManager music;
    private Dictionary<Keys, ICommand> keyMap = new Dictionary<Keys, ICommand>();
    private int levelCount = 0;
    private int levelUpdate = 0;
    private int mLevelUpdate = 15;
    private int minLev;
    private int maxLev;

    public KeyboardCont(Game1 game, IPlayer player, Inventory inventory, MusicManager music)
    {
        this.game = game;
        this.player = player;
        this.inventory = inventory;
        this.music = music;

        minLev = 0;
        maxLev = game.levelManager.Levels.Count;
    }

    public void RegisterCommand()
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

        ICommand pause = new PauseCommand(game);
        keyMap[Keys.Tab] = pause;

        ICommand mute = new MuteMusicCommand(music);
        keyMap[Keys.I] = mute;

        ICommand slot1 = new SwitchInventoryCommand(inventory, 1);
        keyMap[Keys.D1] = slot1;
        ICommand slot2 = new SwitchInventoryCommand(inventory, 2);
        keyMap[Keys.D2] = slot2;
        ICommand slot3 = new SwitchInventoryCommand(inventory, 3);
        keyMap[Keys.D3] = slot3;
        ICommand slot4 = new SwitchInventoryCommand(inventory, 4);
        keyMap[Keys.D4] = slot4;
        ICommand slot5 = new SwitchInventoryCommand(inventory, 5);
        keyMap[Keys.D5] = slot5;
        ICommand slot6 = new SwitchInventoryCommand(inventory, 6);
        keyMap[Keys.D6] = slot6;
        ICommand slot7 = new SwitchInventoryCommand(inventory, 7);
        keyMap[Keys.D7] = slot7;
        ICommand slot8 = new SwitchInventoryCommand(inventory, 8);
        keyMap[Keys.D8] = slot8;
        ICommand slot9 = new SwitchInventoryCommand(inventory, 9);
        keyMap[Keys.D9] = slot9;

    }

    public void HandleAttack()
    {
        KeyboardState kstate = Keyboard.GetState();
               
        if (kstate.IsKeyDown(Keys.N) || kstate.IsKeyDown(Keys.Z))
        {
            keyMap[Keys.Z].Execute(); 
        }
        HandleNoPlayerInput(kstate);
     }

    
    public List<IProjectile> HandlePlayerItem()
    {
        KeyboardState kstate = Keyboard.GetState();
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

        HandleNoPlayerInput(kstate);
        return projs;
    }

    public void HandleMovement()
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

        HandleNoPlayerInput(kstate);

    }


    public void HandleNoPlayerInput(KeyboardState kstate)
    {
        if (kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.A) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.S)
            && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.N) && kstate.IsKeyUp(Keys.Z)
            && kstate.IsKeyUp(Keys.E) && kstate.IsKeyUp(Keys.D1) && kstate.IsKeyUp(Keys.D2) && kstate.IsKeyUp(Keys.D3))
        {
            player.setIdle();
        }
    }

    public void handleLevelSwitch()
    {
        MouseState mouseState = Mouse.GetState();

        levelUpdate++;

        if (levelUpdate > mLevelUpdate)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                levelCount--;

                if (levelCount > minLev && levelCount < maxLev)
                {
                    game.curLevel = game.levelManager.Levels[levelCount];
                    game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
                    game.obstacleHandler.Update();
                    game.randomLevelHandler = new RandomLevelHandler(game, game.blocks);
                    game.randomLevelHandler.Update();

                    game.WallHitboxHandler();
                }
                else
                {
                    levelCount = maxLev - 1;
                }

                levelUpdate = 0;
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                levelCount++;
                if (levelCount > minLev && levelCount < maxLev)
                {
                    game.curLevel = game.levelManager.Levels[levelCount];
                    game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
                    game.obstacleHandler.Update();

                    game.randomLevelHandler = new RandomLevelHandler(game, game.blocks);
                    game.randomLevelHandler.Update();
                    game.WallHitboxHandler();
                }
                else
                {
                    levelCount = 0;
                }
                levelUpdate = 0;
            }
        }
    }

    public void HandleLevelDebug()
    {
        var kstate = Keyboard.GetState();

        if(kstate.IsKeyDown(Keys.B))
        {
            game.curLevel = game.levelManager.Levels[9];
            game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
            game.obstacleHandler.Update();

            game.randomLevelHandler = new RandomLevelHandler(game, game.blocks);
            game.randomLevelHandler.Update();
            game.WallHitboxHandler();
        }
    }

    public void HandleSwitchInventory()
    {
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.D1))
        {
            keyMap[Keys.D1].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D2))
        {
            keyMap[Keys.D2].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D3))
        {
            keyMap[Keys.D3].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D4))
        {
            keyMap[Keys.D4].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D5))
        {
            keyMap[Keys.D5].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D6))
        {
            keyMap[Keys.D6].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D7))
        {
            keyMap[Keys.D7].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D8))
        {
            keyMap[Keys.D8].Execute();
        }
        else if (kstate.IsKeyDown(Keys.D9))
        {
            keyMap[Keys.D9].Execute();
        }

    }

    public void HandlePause()
    {
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Tab))
        {
            keyMap[Keys.Tab].Execute();
        }
    }
    public void HandleMuteMusic()
    {
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.I))
        {
            keyMap[Keys.I].Execute();
        }
    }
}
