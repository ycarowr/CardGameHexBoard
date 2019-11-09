using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [ExecuteInEditMode]
    public class UiPoolPosition : MonoBehaviour, IDataStorage<IUiCardPool>
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

        void Awake()
        {
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