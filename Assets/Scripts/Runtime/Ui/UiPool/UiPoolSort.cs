using Game.Ui;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiPool))]
    public class UiPoolSort : UiGameDataAccess
    {
        private UiPoolPositioning Positioning { get; set; }
        private UiPool UiPool { get; set; }

        protected override void Awake()
        {
            base.Awake();
            UiPool = GetComponent<UiPool>();
            Positioning = new UiPoolPositioning(UiPool);
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var position = UiPool.GetPosition(i);
                position.transform.position = Positioning.GetPositionFor(i);
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            Positioning.Update();
            UpdatePositions();
        }
#endif
    }
}