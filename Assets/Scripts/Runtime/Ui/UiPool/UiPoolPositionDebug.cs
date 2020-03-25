using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionDebug : MonoBehaviour
    {
        [SerializeField] private UiCardSize uiCardSize;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, uiCardSize.Value);
        }
    }
}