using Microsoft.Xna.Framework;


namespace sprint2
{

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
}