using Game.Ui;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiPool))]
    public class UiPoolSort : UiGameDataAccess
    {
        UiPoolPositioning Positioning { get; set; }
        UiPool UiPool { get; set; }

        protected override void Awake()
        {
            base.Awake();
            UiPool = GetComponent<UiPool>();
            Positioning = new UiPoolPositioning(UiPool);
            UpdatePositions();
        }

        void UpdatePositions()
        {
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var position = UiPool.GetPosition(i);
                position.transform.position = Positioning.GetPositionFor(i);
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            Positioning.Update();
            UpdatePositions();
        }
#endif
    }
}