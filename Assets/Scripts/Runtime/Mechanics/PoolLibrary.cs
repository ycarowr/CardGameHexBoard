using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    public class PoolLibrary : BaseGameMechanics
    {
        public PoolLibrary(IGame game) : base(game)
        {
        }

        public void AddCardFromLibrary(IPlayer player, PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;

            var data = Game.Library.GetRandomDataFromPlayer(player.Id);
            var card = new CardPool(data);
        }
    }
}