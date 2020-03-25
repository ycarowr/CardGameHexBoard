using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiHandRegistry))]
    public class UiHandEventHandler : UiEventListener, IDrawCard, IPickCard, ICreateBoardElement, ISelectBoardPosition,
        IRestartGame, ISelectReturnPoolPosition, IReturnCard
    {
        private UiHandRegistry Registry { get; set; }

        void ICreateBoardElement.OnCreateBoardElement(SeatType id, CreatureElement creatureElement, Vector3Int position,
            CardHand card)
        {
            if (!IsMyEvent(id))
                return;

            Registry.RemoveCard(card);
        }

        void IDrawCard.OnDrawCard(SeatType id, CardHand cardHand)
        {
            if (!IsMyEvent(id))
                return;

            Registry.CreateCardFromLibrary(cardHand);
        }

        void IPickCard.OnPickCard(SeatType id, CardHand cardHand, PositionId positionId)
        {
            if (!IsMyEvent(id))
                return;
            Registry.CreateCardFromPool(cardHand, positionId);
        }

        void IRestartGame.OnRestart()
        {
            Registry.Clear();
        }

        void IReturnCard.OnReturnCard(SeatType id, CardHand cardHand, CardPool cardPool, PositionId positionId)
        {
            if (!IsMyEvent(id))
                return;
            Registry.RemoveCard(cardHand);
        }

        void ISelectBoardPosition.OnSelectBoardPosition(Vector3Int position)
        {
            Registry.SelectBoardPosition(position);
        }

        void ISelectReturnPoolPosition.OnSelectReturnPoolPosition(SeatType id, PositionId positionId)
        {
            Registry.ReturnCardToPosition(id, positionId);
        }

        private bool IsMyEvent(SeatType id)
        {
            return Registry.Id == id;
        }

        protected override void Awake()
        {
            base.Awake();
            Registry = GetComponent<UiHandRegistry>();
        }
    }
}