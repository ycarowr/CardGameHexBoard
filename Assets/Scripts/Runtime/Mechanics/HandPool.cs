using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IPickCard
    {
        void OnPickCard(PlayerId id, CardHand card, PositionId positionId);
    }

    [Event]
    public interface IReturnCard
    {
        void OnReturnCard(PlayerId id, CardHand cardHand, CardPool cardPool, PositionId positionId);
    }

    public class HandPool : BaseGameMechanics
    {
        public HandPool(IGame game) : base(game)
        {
        }

        public void PickCard(PlayerId playerId, PositionId positionId)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            var pool = Game.Pool;
            if (pool.IsPositionLocked(positionId))
                return;
            if (!pool.HasDataAt(positionId))
                return;

            var poolCard = Game.Pool.GetAndRemoveCardAt(positionId);
            var data = poolCard.Data;
            var cardHand = new CardHand(data);
            var hand = GetPlayerHand(playerId);
            hand.Add(cardHand);
            OnPickCard(playerId, cardHand, positionId);
        }

        void OnPickCard(PlayerId playerId, CardHand cardHand, PositionId positionId) =>
            Dispatcher.Notify<IPickCard>(i => i.OnPickCard(playerId, cardHand, positionId));

        public void ReturnCard(PlayerId playerId, CardHand cardHand, PositionId positionId)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (Game.Pool.HasDataAt(positionId))
                return;
            var hand = GetPlayerHand(playerId);
            if (!hand.Has(cardHand))
                return;

            hand.Remove(cardHand);
            var data = cardHand.Data;
            var cardPool = new CardPool(data);
            Game.Pool.AddCardAt(cardPool, positionId);
            OnReturnCard(playerId, cardHand, cardPool, positionId);
        }

        void OnReturnCard(PlayerId playerId, CardHand cardHand, CardPool cardPool, PositionId positionId) =>
            Dispatcher.Notify<IReturnCard>(i => i.OnReturnCard(playerId, cardHand, cardPool, positionId));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}