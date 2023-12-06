using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace sprint2
{

    public class MoveUpCommand : ICommand
    {
        IPlayer Player;

        public MoveUpCommand(IPlayer player)
        {
            Player = player;
        }

        public void Execute()
        {
            Player.moveUp();
        }

    }

    public class MoveDownCommand : ICommand
    {
        IPlayer Player;

        public MoveDownCommand(IPlayer player)
        {
            Player = player;
        }

        public void Execute()
        {
            Player.moveDown();
        }

    }

    public class MoveLeftCommand : ICommand
    {
        IPlayer Player;

        public MoveLeftCommand(IPlayer player)
        {
            Player = player;
        }

        public void Execute()
        {
            Player.moveLeft();
        }

    }

    public class MoveRightCommand : ICommand
    {
        IPlayer Player;

        public MoveRightCommand(IPlayer player)
        {
            Player = player;
        }

        public void Execute()
        {
            Player.moveRight();
        }

    }

    public class AttackCommand : ICommand
    {
        private IPlayer Player;

        public AttackCommand(IPlayer player)
        {
            Player = player;
        }
        public void Execute()
        {
            Player.attack();

        }

    }

    public class PauseCommand : ICommand
    {
        private Game1 game;

        public PauseCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.pauseGame();

        }

    }
    public class MuteMusicCommand : ICommand
    {
        private MusicManager music;

        public MuteMusicCommand(MusicManager music)
        {
            //this.game = game;
            this.music = music;
        }
        public void Execute()
        {
            music.MuteMusic();
        }

    }
    public class SwitchInventoryCommand : ICommand
    {
        private Inventory inventory;
        private int inventorySlot;

        public SwitchInventoryCommand(Inventory inventory, int code)
        {
            this.inventory = inventory;
            this.inventorySlot = code;
        }
        public void Execute()
        {
            inventory.handleSwitchInventory(inventorySlot);

        }

    }

   
}