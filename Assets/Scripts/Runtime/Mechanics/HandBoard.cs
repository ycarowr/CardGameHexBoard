using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IPlayCard
    {
        void OnPreGameStart(IPlayer[] players);
    }

    public class HandBoard : BaseGameMechanics
    {
        public HandBoard(IGame game) : base(game)
        {
        }

        public void PlayCardAt(IPlayer player, CardHand card, Vector2Int position)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (!Game.TurnLogic.IsMyTurn(player))
                return;

            var hand = GetPlayerHand(player.Id);
            if (!hand.Has(card))
                return;

            hand.Remove(card);
            var cardBoard = new CardBoard(card.Data);
            Game.Board.AddDataAt(cardBoard, position);
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