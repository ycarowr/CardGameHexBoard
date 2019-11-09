using System;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [ExecuteInEditMode]
    public class UiPoolPosition : MonoBehaviour, IDataStorage<IUiCardPool>
    {
        [SerializeField] UiPoolParameters parameters;
        [SerializeField] PositionId id;
        public PositionId Id => id;
        public bool HasData => Data != null;
        public IUiCardPool Data { get; private set; }
        BoxCollider2D Collider { get; set; }

        void OnEnable() 
        {
            Collider = GetComponent<BoxCollider2D>();
            Collider.size = parameters.UiCardSize.Value;
        }

        public void SetData(IUiCardPool value)
        {
            Data = value;
            Data?.Motion.MoveTo(transform.position, parameters.PoolMovementSpeed);
        }

        public void Clear()
        {
            Destroy(Data.MonoBehaviour.gameObject);
            SetData(null);
        }
    }
}