using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface ICreateCreature
    {
        void OnCreateCreature(PlayerId id, Creature creature, Vector2Int position, CardHand card);
    }

    public class HandBoard : BaseGameMechanics
    {
        public HandBoard(IGame game) : base(game)
        {
        }

        public void CreateCreatureAt(IPlayer player, CardHand card, Vector2Int position)
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
            var creature = new Creature(card.Data);
            Game.Board.AddDataAt(creature, position);
            OnCreateCreature(player.Id, creature, position, card);
        }

        void OnCreateCreature(PlayerId playerId, Creature creature, Vector2Int position, CardHand card) =>
            Dispatcher.Notify<ICreateCreature>(i => i.OnCreateCreature(playerId, creature, position, card));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}