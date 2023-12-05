namespace sprint2
{
    public class ItemCommand : ICommand
    {
        private IPlayer Player;

        public ItemCommand(IPlayer player)
        {
            Player = player;
        }
        public void Execute()
        {
            Player.attack();

        }
    }
            
}

