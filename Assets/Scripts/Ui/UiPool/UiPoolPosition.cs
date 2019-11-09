using Game.Ui;
using HexCardGame.Runtime.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    [ExecuteInEditMode]
    public class UiPoolPosition : UiEventListener, IDataStorage<IUiCardPool>, ILockPosition, IUnlockPosition
    {
        [SerializeField] PositionId id;
        [SerializeField] UiPoolParameters parameters;
        public PositionId Id => id;
        BoxCollider2D Collider { get; set; }
        public bool HasData => Data != null;
        public IUiCardPool Data { get; private set; }

        public void SetData(IUiCardPool value)
        {
            Data = value;
            Data?.Motion.MoveTo(transform.position, parameters.PoolMovementSpeed);
        }

        void ILockPosition.OnLockPosition(PositionId positionId)
        {
            if (id == positionId)
            {
                Logger.Log<ILockPosition>("Locked: " + positionId);
                Data?.SetColor(parameters.Locked);
            }
        }

        void IUnlockPosition.OnUnlockPosition(PositionId positionId)
        {
            if (id == positionId) Data?.SetColor(parameters.Unlocked);
        }

        protected override void Awake()
        {
            base.Awake();
            Collider = GetComponent<BoxCollider2D>();
            Collider.size = parameters.UiCardSize.Value;
        }

        public void Clear()
        {
            if (HasData)
            {
                ObjectPooler.Instance.Release(Data.MonoBehaviour.gameObject);
                SetData(null);
            }
        }
    }
}