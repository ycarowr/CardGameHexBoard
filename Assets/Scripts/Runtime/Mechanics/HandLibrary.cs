namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IDrawCard
    {
        void OnDrawCard(PlayerId id, CardHand card);
    }

    public class HandLibrary : BaseGameMechanics
    {
        public HandLibrary(IGame game) : base(game)
        {
        }

        public void FreeDrawCard(PlayerId playerId)
        {
            if (!Game.IsGameStarted)
                return;

            var data = Game.Library.GetRandomData();
            var card = new CardHand(data);
            var hand = GetPlayerHand(playerId);
            hand.Add(card);
            OnDrawCard(playerId, card);
        }

        public void DrawCard(PlayerId playerId)
        {
            if (!Game.IsGameStarted)
                return;

            var data = Game.Library.GetRandomData();
            var card = new CardHand(data);
            var hand = GetPlayerHand(playerId);
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var inventory = GetInventory(playerId);
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            hand.Add(card);
            OnDrawCard(playerId, card);
        }

        void OnDrawCard(PlayerId playerId, CardHand card) =>
            Dispatcher.Notify<IDrawCard>(i => i.OnDrawCard(playerId, card));
    }
}