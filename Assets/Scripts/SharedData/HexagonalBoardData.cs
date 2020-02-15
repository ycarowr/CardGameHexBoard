using System.Collections.Generic;
using HexBoardGame.Runtime;
using HexCardGame.Runtime;
using UnityEngine;

namespace HexCardGame.SharedData
{
    public abstract class BoardData : ScriptableObject
    {
        public abstract Hex[] GetHexPoints();
    }

    [CreateAssetMenu(menuName = "Data/BoardData", fileName = "HexagonalBoardData")]
    public class HexagonalBoardData : BoardData
    {
        [Range(1, 10)] public int radius;
        
        public override Hex[] GetHexPoints()
        {
            var points = new List<Hex>();
            for (var x = -radius; x <= radius; x++)
            {
                var yMin = Mathf.Max(-radius, -x - radius);
                var yMax = Mathf.Min(radius, -x + radius);
                for (var y = yMin; y <= yMax; y++)
                    points.Add(new Hex(x, y));
            }

            return points.ToArray();
        }
    }
}