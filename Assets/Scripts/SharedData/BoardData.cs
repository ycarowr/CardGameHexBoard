using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Data/BoardData", fileName = "BoardData")]
    public class BoardData : ScriptableObject
    {
        [Range(1, 10)] public int MaxX;
        [Range(1, 10)] public int MaxY;
        public Vector2Int[] UndesiredPositions;

        public Vector2Int[] GetDesiredPositions()
        {
            var size = MaxX * MaxY;
            size -= UndesiredPositions.Length;
            var desired = new Vector2Int[size];

            var count = 0;
            for (var i = 0; i < MaxX; i++)
            for (var j = 0; j < MaxY; j++)
            {
                var position = new Vector2Int(i, j);
                if (UndesiredContains(position))
                    continue;

                desired[count] = position;
                ++count;
            }

            return desired;
        }

        public bool UndesiredContains(Vector2Int position)
        {
            foreach (var i in UndesiredPositions)
                if (i.x == position.x && i.y == position.y)
                    return true;

            return false;
        }
    }
}