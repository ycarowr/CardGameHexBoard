using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolDebug : MonoBehaviour
    {
        [SerializeField] UiPoolParameters parameters;
        [SerializeField] UiPool uiPool;
        UiPoolPositioning Positioning { get; set; }
        void OnDrawGizmos() => DrawBoundaries();

        void DrawBoundaries()
        {
            if (Positioning == null)
                Positioning = new UiPoolPositioning(uiPool);
            else
                Positioning.Update();
            DrawSquare();
            var positions = PoolPositionUtility.GetAllIndices();
            Gizmos.color = Color.green;
            foreach (var i in positions)
            {
                var center = Positioning.GetPositionFor(i);
                uiPool.GetPosition(i).transform.position = center;
                Gizmos.DrawCube(center, Positioning.Size);
            }
        }

        void DrawSquare()
        {
            //boundaries
            Gizmos.DrawLine(new Vector3(Positioning.MinX, Positioning.MinY),
                new Vector3(Positioning.MaxX, Positioning.MinY));
            Gizmos.DrawLine(new Vector3(Positioning.MinX, Positioning.MaxY),
                new Vector3(Positioning.MaxX, Positioning.MaxY));
            Gizmos.DrawLine(new Vector3(Positioning.MinX, Positioning.MinY),
                new Vector3(Positioning.MinX, Positioning.MaxY));
            Gizmos.DrawLine(new Vector3(Positioning.MaxX, Positioning.MinY),
                new Vector3(Positioning.MaxX, Positioning.MaxY));
        }
    }
}