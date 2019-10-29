using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Data/BoardData", fileName = "BoardData")]
    public class BoardData : ScriptableObject
    {
        [Range(1, 10)] public int MaxX;
        [Range(1, 10)] public int MaxY;
        public Vector2Int[] UndesiredPositions;
    }
}