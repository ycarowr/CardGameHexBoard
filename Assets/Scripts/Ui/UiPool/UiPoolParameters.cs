using UnityEngine;

namespace HexCardGame.UI
{
    [CreateAssetMenu(menuName = "Variables/PoolParameters")]
    public class UiPoolParameters : ScriptableObject
    {
        [SerializeField] [Range(0, 5)] float spacingX;
        [SerializeField] [Range(0, 5)] float spacingY;

        public float SpacingY => spacingY;

        public float SpacingX => spacingX;
    }
}