using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiHandRegistry))]
    public class UiHandEventHandler : UiEventListener, IDrawCard, IPickCard, ICreateBoardElement, ISelectBoardPosition,
        IRestartGame, ISelectPoolPosition, IReturnCard
    {
        UiHandRegistry Registry { get; set; }

        void ICreateBoardElement.OnCreateBoardElement(PlayerId id, BoardElement boardElement, Vector3Int position,
            CardHand card)
        {
            if (!IsMyEvent(id))
                return;

            Registry.RemoveCard(card);
        }

        void IDrawCard.OnDrawCard(PlayerId id, CardHand cardHand)
        {
            if (!IsMyEvent(id))
                return;

            Registry.CreateCardFromLibrary(cardHand);
        }

        void IPickCard.OnPickCard(PlayerId id, CardHand cardHand, PositionId positionId)
        {
            if (!IsMyEvent(id))
                return;

            Registry.CreateCardFromPool(cardHand, positionId);
        }

        void IRestartGame.OnRestart() => Registry.Clear();

        void IReturnCard.OnReturnCard(PlayerId id, CardHand cardHand, CardPool cardPool, PositionId positionId)
        {
            if (!IsMyEvent(id))
                return;

            Registry.RemoveCard(cardHand);
        }

        void ISelectBoardPosition.OnSelectBoardPosition(Vector3Int position) => Registry.SelectBoardPosition(position);

        void ISelectPoolPosition.OnSelectPoolPosition(PlayerId id, PositionId positionId) =>
            Registry.SelectPoolPosition(id, positionId);

        bool IsMyEvent(PlayerId id) => Registry.Id == id;

        protected override void Awake()
        {
            base.Awake();
            Registry = GetComponent<UiHandRegistry>();
        }
    }
}