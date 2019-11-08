using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface ICreateBoardElement
    {
        void OnCreateBoardElement(PlayerId id, BoardElement boardElement, Vector2Int position, CardHand card);
    }

    public class HandBoard : BaseGameMechanics
    {
        public HandBoard(IGame game) : base(game)
        {
        }

        public void CreateBoardElementAt(PlayerId playerId, CardHand card, Vector2Int position)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (!Game.TurnLogic.IsMyTurn(playerId))
                return;

            var hand = GetPlayerHand(playerId);
            if (!hand.Has(card))
                return;

            hand.Remove(card);
            var creature = new BoardElement(card.Data, playerId);
            Game.Board.AddDataAt(creature, position);
            OnCreateCreature(playerId, creature, position, card);
        }

        void OnCreateCreature(PlayerId playerId, BoardElement boardElement, Vector2Int position, CardHand card) =>
            Dispatcher.Notify<ICreateBoardElement>(i => i.OnCreateBoardElement(playerId, boardElement, position, card));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}