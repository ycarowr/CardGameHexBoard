using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionDebug : MonoBehaviour
    {
        [SerializeField] Collider2D cardTemplate;
        Vector3 Size => cardTemplate.bounds.size;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, Size);
        }
    }
}