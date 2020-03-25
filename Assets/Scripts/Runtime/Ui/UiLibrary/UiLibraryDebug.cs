using UnityEngine;

namespace HexCardGame.UI
{
    public class UiLibraryDebug : MonoBehaviour
    {
        [SerializeField] private UiCardSize cardSize;

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, cardSize.Value);
        }
    }
}