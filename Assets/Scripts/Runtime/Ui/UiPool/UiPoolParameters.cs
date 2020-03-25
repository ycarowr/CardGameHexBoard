using UnityEngine;

namespace HexCardGame.UI
{
    [CreateAssetMenu(menuName = "Variables/PoolParameters")]
    public class UiPoolParameters : ScriptableObject
    {
        [SerializeField] private Color locked;
        [SerializeField, Range(0.1f, 10)] private float poolMovementSpeed;
        [SerializeField, Range(0, 5)] private float spacingX;
        [SerializeField, Range(0, 5)] private float spacingY;
        [SerializeField] private UiCardPool uiCardPoolTemplate;
        [SerializeField] private UiCardSize uiCardSize;
        [SerializeField] private Color unlocked;

        public float SpacingY => spacingY;

        public float SpacingX => spacingX;
        public UiCardSize UiCardSize => uiCardSize;

        public float PoolMovementSpeed => poolMovementSpeed;

        public UiCardPool UiCardPoolTemplate => uiCardPoolTemplate;

        public Color Locked => locked;

        public Color Unlocked => unlocked;
    }
}