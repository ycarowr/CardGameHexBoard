using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionDebug : MonoBehaviour
    {
        [SerializeField] UiCardSize uiCardSize;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, uiCardSize.Value);
        }
    }
}