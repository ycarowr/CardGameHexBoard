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
            var hasEnoughGold = inventory.GetAmount(Inventory.GoldItem) >= cost;
            if (!hasEnoughGold)
                return;
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            inventory.RemoveItem(Inventory.GoldItem, cost);
            hand.Remove(card);
            var creature = new CreatureElement(card.Data, playerId);
            Game.Board.AddDataAt(creature, position);
            OnCreateCreature(playerId, creature, position, card);
        }

        void OnCreateCreature(PlayerId playerId, CreatureElement creatureElement, Vector3Int position, CardHand card) =>
            Dispatcher.Notify<ICreateBoardElement>(i =>
                i.OnCreateBoardElement(playerId, creatureElement, position, card));
    }
}