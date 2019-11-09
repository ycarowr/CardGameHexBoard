using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPosition : MonoBehaviour, IDataStorage<IUiCardPool>
    {
        const float PoolMovementSpeed = 10;
        [SerializeField] PositionId id;
        public PositionId Id => id;
        public bool HasData => Data != null;
        public IUiCardPool Data { get; private set; }

        public void SetData(IUiCardPool value)
        {
            Data = value;
            Data?.Motion.MoveTo(transform.position, PoolMovementSpeed);
        }

        public void Clear()
        {
            Destroy(Data.MonoBehaviour.gameObject);
            SetData(null);
        }
    }
}