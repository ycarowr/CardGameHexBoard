using UnityEngine;

namespace HexCardGame.UI
{
    public class UiLibraryDebug : MonoBehaviour
    {
        [SerializeField] UiCardSize cardSize;

        void OnDrawGizmos() => Gizmos.DrawCube(transform.position, cardSize.Value);
    }
}