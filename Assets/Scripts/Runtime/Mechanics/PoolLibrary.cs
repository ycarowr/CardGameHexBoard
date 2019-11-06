using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    public class PoolLibrary : BaseGameMechanics
    {
        public PoolLibrary(IGame game) : base(game)
        {
        }

        public void RevealCard(IPlayer player, PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;

            var data = Game.Library.GetRandomData();
            var cardPool = new CardPool(data);
            OnRevealCard(player.Id, cardPool, positionIndex);
        }

        void OnRevealCard(PlayerId playerId, CardPool cardPool, PoolPositionIndex positionIndex) =>
            Dispatcher.Notify<IRevealCard>(i => i.OnRevealCard(playerId, cardPool, positionIndex));

        [Event]
        public interface IRevealCard
        {
            void OnRevealCard(PlayerId id, CardPool cardPool, PoolPositionIndex positionIndex);
        }
    }
}