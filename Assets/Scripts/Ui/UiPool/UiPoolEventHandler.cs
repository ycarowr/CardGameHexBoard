using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiPool))]
    public class UiPoolEventHandler : UiEventListener, IRestartGame, ISelectPickPoolPosition,
        IPickCard, IReturnCard,
        IRevealCard, IRevealPool
    {
        UiPool UiPool { get; set; }

        void IPickCard.OnPickCard(PlayerId id, CardHand cardHand, PositionId positionId)
        {
            Logger.Log<UiPoolEventHandler>("Pick received");
            UiPool.RemoveCard(positionId);
        }

        void IRestartGame.OnRestart() => UiPool.Clear();

        void IReturnCard.OnReturnCard(PlayerId id, CardHand cardHand, CardPool cardPool, PositionId positionId)
        {
            Logger.Log<UiPoolEventHandler>("return received");
            UiPool.AddCard(cardPool, positionId);
        }

        void IRevealCard.OnRevealCard(PlayerId id, CardPool cardPool, PositionId positionId) =>
            UiPool.AddCard(cardPool, positionId);

        void IRevealPool.OnRevealPool(IPool<CardPool> pool)
        {
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var cardPool = pool.GetCardAt(i);
                UiPool.AddCard(cardPool, i);
            }
        }

        void ISelectPickPoolPosition.OnSelectPickPoolPosition(PlayerId playerId, PositionId positionId) =>
            GameData.CurrentGameInstance.PickCardFromPosition(PlayerId.User, positionId);

        protected override void Awake()
        {
            base.Awake();
            UiPool = GetComponent<UiPool>();
        }
    }
}