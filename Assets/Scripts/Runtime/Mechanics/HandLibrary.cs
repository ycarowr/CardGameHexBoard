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

        public void DrawCard(IPlayer player)
        {
            if (!Game.IsGameStarted)
                return;

            var data = Game.Library.GetRandomData();
            var card = new CardHand(data);
            var playerHand = GetPlayerHand(player.Id);
            playerHand.Add(card);
            OnDrawCard(player.Id, card);
        }

        void OnDrawCard(PlayerId playerId, CardHand card) =>
            Dispatcher.Notify<IDrawCard>(i => i.OnDrawCard(playerId, card));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}