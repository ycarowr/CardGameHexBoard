using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPool : UiGameInputRequester
    {
        UiPoolPositioning _positioning;
        [SerializeField] Transform libraryPosition;
        [SerializeField] UiPoolParameters parameters;
        [SerializeField] UiPoolPosition[] poolCardPositions;
        IPool<CardPool> CurrentPool => GameData.CurrentGameInstance.Pool;

        public void PickCard(PositionId positionId)
        {
            var position = GetPosition(positionId);
            if (!position.HasData)
                return;

            position.Clear();
        }

        public void ReturnCard(CardPool cardPool, PositionId positionId)
        {
            var isLocked = CurrentPool.IsPositionLocked(positionId);
            AddCard(cardPool, positionId, isLocked);
        }

        public void RevealCard(CardPool cardPool, PositionId positionId) =>
            AddCard(cardPool, positionId, CurrentPool.IsPositionLocked(positionId));

        public void RevealPool(IPool<CardPool> pool)
        {
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var cardPool = pool.GetCardAt(i);
                var isLocked = pool.IsPositionLocked(i);
                AddCard(cardPool, i, isLocked);
            }
        }

        public void SelectPoolPosition(PositionId positionId) =>
            GameData.CurrentGameInstance.PickCardFromPosition(PlayerId.User, positionId);

        public void Clear()
        {
            foreach (var i in poolCardPositions)
                i.Clear();
        }

        void AddCard(CardPool cardPool, PositionId positionId, bool isLocked)
        {
            var uiPosition = GetPosition(positionId);
            var template = parameters.UiCardPoolTemplate.gameObject;
            var uiCard = ObjectPooler.Instance.Get<UiCardPool>(template);
            uiCard.SetAndUpdateView(cardPool.Data);
            if (isLocked)
                uiCard.SetColor(parameters.Locked);
            uiCard.transform.position = libraryPosition.transform.position;
            uiCard.transform.SetParent(uiPosition.transform);
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