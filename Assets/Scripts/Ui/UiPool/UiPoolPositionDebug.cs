using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionDebug : MonoBehaviour
    {
        [SerializeField] Transform cardTemplate;
        Vector3 Size => cardTemplate.localScale;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, Size);
        }
    }
}