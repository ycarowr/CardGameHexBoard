using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiPool))]
    public class UiPoolEventHandler : UiEventListener, IRestartGame, ISelectPoolPosition,
        IPickCard, IReturnCard,
        IRevealCard, IRevealPool
    {
        UiPool UiPool { get; set; }

        void IPickCard.OnPickCard(PlayerId id, CardHand cardHand, PositionId positionId) => UiPool.PickCard(positionId);

        void IRestartGame.OnRestart() => UiPool.Clear();

        void IReturnCard.OnReturnCard(PlayerId id, CardHand cardHand, CardPool cardPool, PositionId positionId) =>
            UiPool.ReturnCard(cardPool, positionId);

        void IRevealCard.OnRevealCard(PlayerId id, CardPool cardPool, PositionId positionId) =>
            UiPool.RevealCard(cardPool, positionId);

        void IRevealPool.OnRevealPool(IPool<CardPool> pool) => UiPool.RevealPool(pool);

        void ISelectPoolPosition.OnSelectPoolPosition(PlayerId playerId, PositionId positionId) =>
            UiPool.SelectPoolPosition(positionId);

        protected override void Awake()
        {
            base.Awake();
            UiPool = GetComponent<UiPool>();
        }
    }
}