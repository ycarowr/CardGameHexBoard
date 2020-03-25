using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPool : UiGameDataAccess
    {
        [SerializeField] private Transform libraryPosition;
        [SerializeField] private UiPoolParameters parameters;
        [SerializeField] private UiPoolPosition[] poolCardPositions;
        private IPool<CardPool> CurrentPool => GameData.CurrentGameInstance.Pool;

        public UiPoolPosition[] PoolCardPositions => poolCardPositions;

        public UiPoolParameters Parameters => parameters;

        private bool IsPositionLocked(PositionId positionId)
        {
            return CurrentPool.IsPositionLocked(positionId);
        }

        public void RemoveCard(PositionId positionId)
        {
            var position = GetPosition(positionId);
            if (!position.HasData)
                return;

            position.Clear();
        }

        public void AddCard(CardPool cardPool, PositionId positionId, bool fromLibAnimation = true)
        {
            var uiPosition = GetPosition(positionId);
            var template = Parameters.UiCardPoolTemplate.gameObject;
            var uiCard = ObjectPooler.Instance.Get<UiCardPool>(template);
            uiCard.SetAndUpdateView(cardPool.Data);
            if (IsPositionLocked(positionId))
                uiCard.SetColor(Parameters.Locked);
            if (fromLibAnimation)
                uiCard.transform.position = libraryPosition.transform.position;
            else
                uiCard.transform.position = uiPosition.transform.position;

            uiCard.transform.SetParent(uiPosition.transform);
            uiPosition.SetData(uiCard);
        }

        public UiPoolPosition GetPosition(PositionId positionId)
        {
            foreach (var i in PoolCardPositions)
                if (i.Id == positionId)
                    return i;
            return null;
        }

        public void Clear()
        {
            foreach (var i in PoolCardPositions)
                i.Clear();
        }
    }
}