namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IDrawCard
    {
        void OnDrawCard(SeatType id, CardHand card);
    }

    public class HandLibrary : BaseGameMechanics
    {
        public HandLibrary(IGame game) : base(game)
        {
        }

        public void FreeDrawCard(SeatType seatType)
        {
            if (!Game.IsGameStarted)
                return;

            var hand = GetPlayerHand(seatType);
            var handSize = hand.Cards.Count;
            if (handSize >= hand.MaxHandSize)
                return;
            
            var data = Game.Library.GetRandomData();
            var card = new CardHand(data);
            hand.Add(card);
            OnDrawCard(seatType, card);
        }

        public void DrawCard(SeatType seatType)
        {
            if (!Game.IsGameStarted)
                return;
            var hand = GetPlayerHand(seatType);
            var handSize = hand.Cards.Count;
            if(handSize >= hand.MaxHandSize)
                return;

            var data = Game.Library.GetRandomData();
            var card = new CardHand(data);
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var inventory = GetInventory(seatType);
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            hand.Add(card);
            OnDrawCard(seatType, card);
        }

        void OnDrawCard(SeatType seatType, CardHand card) =>
            Dispatcher.Notify<IDrawCard>(i => i.OnDrawCard(seatType, card));
    }
}