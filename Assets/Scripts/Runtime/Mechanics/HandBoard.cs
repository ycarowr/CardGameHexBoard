using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface ICreateBoardElement
    {
        void OnCreateBoardElement(SeatType id, CreatureElement creatureElement, Vector3Int position, CardHand card);
    }

    public class HandBoard : BaseGameMechanics
    {
        public HandBoard(IGame game) : base(game)
        {
        }

        public void PlayCardAt(SeatType seatType, CardHand card, Vector3Int position)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.IsTurnInProgress)
                return;
            if (!Game.TurnLogic.IsMyTurn(seatType))
                return;

            var hand = GetPlayerHand(seatType);
            if (!hand.Has(card))
                return;
            var cost = card.Cost;
            var inventory = GetInventory(seatType);
            var hasEnoughGold = inventory.GetAmount(Inventory.GoldItem) >= cost;
            if (!hasEnoughGold)
                return;
            var actionPoints = Parameters.Amounts.ActionPointsConsume;
            var hasEnoughActionPoints = inventory.GetAmount(Inventory.ActionPointItem) >= actionPoints;
            if (!hasEnoughActionPoints)
                return;

            var isPositionBusy = Game.Board.GetPosition(position).HasData;
            if (isPositionBusy)
                return;

            inventory.RemoveItem(Inventory.ActionPointItem, actionPoints);
            inventory.RemoveItem(Inventory.GoldItem, cost);
            hand.Remove(card);
            var creature = new CreatureElement(card.Data, seatType);
            Game.Board.AddDataAt(creature, position);
            OnCreateCreature(seatType, creature, position, card);
        }

        private void OnCreateCreature(SeatType seatType, CreatureElement creatureElement, Vector3Int position,
            CardHand card)
        {
            Dispatcher.Notify<ICreateBoardElement>(i =>
                i.OnCreateBoardElement(seatType, creatureElement, position, card));
        }
    }
}