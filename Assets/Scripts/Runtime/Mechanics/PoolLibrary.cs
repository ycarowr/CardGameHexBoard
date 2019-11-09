using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IRevealCard
    {
        void OnRevealCard(PlayerId id, CardPool cardPool, PositionId positionId);
    }

    public class PoolLibrary : BaseGameMechanics
    {
        public PoolLibrary(IGame game) : base(game)
        {
        }

        public void RevealCard(PlayerId playerId, PositionId positionId)
        {
            if (!Game.IsGameStarted)
                return;

            var pool = Game.Pool;
            var isPositionLocked = pool.IsPositionLocked(positionId);
            if (isPositionLocked)
                return;

            var data = Game.Library.GetRandomData();
            var cardPool = new CardPool(data);
            pool.AddCardAt(cardPool, positionId);
            OnRevealCard(playerId, cardPool, positionId);
        }

        void OnRevealCard(PlayerId playerId, CardPool cardPool, PositionId positionId) =>
            Dispatcher.Notify<IRevealCard>(i => i.OnRevealCard(playerId, cardPool, positionId));

        public void RevealCardHigherPosition(PlayerId playerId)
        {
            if (!Game.IsGameStarted)
                return;

            var pool = Game.Pool;
            var empty = FindEmpty();
            if (!empty.HasValue)
                return;

            var data = Game.Library.GetRandomData();
            var cardPool = new CardPool(data);
            pool.AddCardAt(cardPool, empty.Value);
            OnRevealCard(playerId, cardPool, empty.Value);
        }

        PositionId? FindEmpty()
        {
            var pool = Game.Pool;
            var positions = PoolPositionUtility.GetAllIndices();
            for (var i = positions.Length - 1; i >= 0; i--)
                if (!pool.HasDataAt(positions[i]))
                    return positions[i];
            return null;
        }
    }
}