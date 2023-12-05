using Microsoft.Xna.Framework;

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
}