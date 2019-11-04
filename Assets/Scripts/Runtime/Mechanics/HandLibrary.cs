namespace HexCardGame.Runtime.Game
{
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
        }

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}