using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    public class HandPool : BaseGameMechanics
    {
        public HandPool(IGame game) : base(game)
        {
        }

        public void PickCard(IPlayer player, PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (!Game.Pool.HasDataAt(positionIndex))
                return;

            var poolCard = Game.Pool.GetAndRemoveCardAt(positionIndex);
            var data = poolCard.Data;
            var cardHand = new CardHand(data);
            var hand = GetPlayerHand(player.Id);
            hand.Add(cardHand);
            OnPickCard(player.Id, cardHand, positionIndex);
        }

        void OnPickCard(PlayerId playerId, CardHand cardHand, PoolPositionIndex positionIndex) =>
            Dispatcher.Notify<IPickCard>(i => i.OnPickCard(playerId, cardHand, positionIndex));

        public void ReturnCard(IPlayer player, CardHand cardHand, PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (Game.Pool.HasDataAt(positionIndex))
                return;
            var hand = GetPlayerHand(player.Id);
            if (!hand.Has(cardHand))
                return;

            hand.Remove(cardHand);
            var data = cardHand.Data;
            var cardPool = new CardPool(data);
            Game.Pool.AddCardAt(cardPool, positionIndex);
            OnReturnCard(player.Id, cardHand, positionIndex);
        }

        void OnReturnCard(PlayerId playerId, CardHand cardHand, PoolPositionIndex positionIndex) =>
            Dispatcher.Notify<IReturnCard>(i => i.OnReturnCard(playerId, cardHand, positionIndex));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }

        [Event]
        public interface IPickCard
        {
            void OnPickCard(PlayerId id, CardHand card, PoolPositionIndex positionIndex);
        }

        [Event]
        public interface IReturnCard
        {
            void OnReturnCard(PlayerId id, CardHand cardHand, PoolPositionIndex positionIndex);
        }
    }
}