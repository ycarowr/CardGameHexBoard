using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface ICreateBoardElement
    {
        void OnCreateBoardElement(PlayerId id, CreatureElement creatureElement, Vector3Int position, CardHand card);
    }

    public class HandBoard : BaseGameMechanics
    {
        public HandBoard(IGame game) : base(game)
        {
        }

        public void PlayCardAt(PlayerId playerId, CardHand card, Vector3Int position)
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
            var cost = card.Cost;
            var inventory = GetInventory(playerId);
            var hasEnoughGold = inventory.GetAmount(Gold.Id) >= cost;
            if (!hasEnoughGold)
                return;
            inventory.RemoveItem(Gold.Id, cost);
            
            hand.Remove(card);
            var creature = new CreatureElement(card.Data, playerId);
            Game.Board.AddDataAt(creature, position);
            OnCreateCreature(playerId, creature, position, card);
        }

        void OnCreateCreature(PlayerId playerId, CreatureElement creatureElement, Vector3Int position, CardHand card) =>
            Dispatcher.Notify<ICreateBoardElement>(i => i.OnCreateBoardElement(playerId, creatureElement, position, card));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }

        IInventory GetInventory(PlayerId id)
        {
            foreach (var i in Game.Inventories)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}