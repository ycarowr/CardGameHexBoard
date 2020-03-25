using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IPickCard
    {
        void OnPickCard(SeatType id, CardHand cardHand, PositionId positionId);
    }

    [Event]
    public interface IReturnCard
    {
        void OnReturnCard(SeatType id, CardHand cardHand, CardPool cardPool, PositionId positionId);
    }

    public class HandPool : BaseGameMechanics
    {
        public HandPool(IGame game) : base(game)
        {
        }

        public void PickCard(SeatType seatType, PositionId positionId)
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
            var isMyTurn = Game.TurnLogic.IsMyTurn(seatType);
            if (!isMyTurn)
                return;
            var hand = GetPlayerHand(seatType);
            var handSize = hand.Cards.Count;
            if (handSize >= hand.MaxHandSize)
                return;
            var inventory = GetInventory(seatType);
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            var poolCard = Game.Pool.GetAndRemoveCardAt(positionId);
            var data = poolCard.Data;
            var cardHand = new CardHand(data);
            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            hand.Add(cardHand);
            OnPickCard(seatType, cardHand, positionId);
        }

        private void OnPickCard(SeatType seatType, CardHand cardHand, PositionId positionId)
        {
            Dispatcher.Notify<IPickCard>(i => i.OnPickCard(seatType, cardHand, positionId));
        }

        public void ReturnCard(SeatType seatType, CardHand cardHand, PositionId positionId)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (Game.Pool.HasDataAt(positionId))
                return;
            var hand = GetPlayerHand(seatType);
            if (!hand.Has(cardHand))
                return;
            var isMyTurn = Game.TurnLogic.IsMyTurn(seatType);
            if (!isMyTurn)
                return;
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var inventory = GetInventory(seatType);
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            hand.Remove(cardHand);
            var data = cardHand.Data;
            var cardPool = new CardPool(data);
            Game.Pool.AddCardAt(cardPool, positionId);
            OnReturnCard(seatType, cardHand, cardPool, positionId);
        }

        private void OnReturnCard(SeatType seatType, CardHand cardHand, CardPool cardPool, PositionId positionId)
        {
            Dispatcher.Notify<IReturnCard>(i => i.OnReturnCard(seatType, cardHand, cardPool, positionId));
        }
    }
}