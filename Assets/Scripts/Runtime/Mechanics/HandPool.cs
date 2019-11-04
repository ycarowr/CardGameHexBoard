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

            var poolCard = Game.Pool.GetCardAt(positionIndex);
            if (poolCard.IsCovered)
                return;

            var data = poolCard.Data;
            var cardHand = new CardHand(data);
            var hand = GetPlayerHand(player.Id);
            hand.Add(cardHand);
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