using UnityEngine;

namespace HexCardGame.UI
{
    [CreateAssetMenu(menuName = "Variables/PoolParameters")]
    public class UiPoolParameters : ScriptableObject
    {
        [SerializeField] [Range(0.1f, 10)] float poolMovementSpeed;
        [SerializeField] [Range(0, 5)] float spacingX;
        [SerializeField] [Range(0, 5)] float spacingY;
        [SerializeField] UiCardPool uiCardPoolTemplate;
        [SerializeField] UiCardSize uiCardSize;

        public float SpacingY => spacingY;

        public float SpacingX => spacingX;
        public UiCardSize UiCardSize => uiCardSize;

        public float PoolMovementSpeed => poolMovementSpeed;

        public UiCardPool UiCardPoolTemplate => uiCardPoolTemplate;
    }
}