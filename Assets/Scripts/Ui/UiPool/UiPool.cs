using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiPool : UiEventListener,
        IPickCard, IReturnCard,
        IRevealCard, IRevealPool
    {
        UiPoolPositioning _positioning;
        [SerializeField] UiCardPool cardPoolTemplate;
        [SerializeField] Transform deckPosition;
        [SerializeField] UiPoolParameters parameters;
        [SerializeField] UiPoolPosition[] poolCardPositions;
        public Vector2 CardSize => cardPoolTemplate.Size;

        void IPickCard.OnPickCard(PlayerId id, CardHand card, PositionId positionId) =>
            Logger.Log<UiPool>("pick Card Received", Color.blue);

        void IReturnCard.OnReturnCard(PlayerId id, CardHand cardHand, PositionId positionId) =>
            Logger.Log<UiPool>("Return Card Received", Color.blue);

        void IRevealCard.OnRevealCard(PlayerId id, CardPool cardPool, PositionId positionId) =>
            Logger.Log<UiPool>("Reveal Card received", Color.blue);

        void IRevealPool.OnRevealPool(IPool<CardPool> pool)
        {
            Logger.Log<UiPool>("On Reveal Pool received", Color.blue);

            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var cardHand = pool.GetCardAt(i);
                AddCard(cardHand, i);
            }
        }

        void AddCard(CardPool cardPool, PositionId positionId)
        {
            var uiPosition = GetPosition(positionId);
            var uiCard = Instantiate(cardPoolTemplate, deckPosition.position, Quaternion.identity,
                uiPosition.transform);
            uiPosition.SetData(uiCard);
        }

        public UiPoolPosition GetPosition(PositionId positionId)
        {
            foreach (var i in poolCardPositions)
                if (i.Id == positionId)
                    return i;
            return null;
        }

        protected override void Awake()
        {
            base.Awake();
            _positioning = new UiPoolPositioning(this, parameters);
            UpdatePositions();
        }

        void UpdatePositions()
        {
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var position = GetPosition(i);
                position.transform.position = _positioning.GetPositionFor(i);
            }
        }

        void Update()
        {
            _positioning.Update();
            UpdatePositions();
        }
    }
}