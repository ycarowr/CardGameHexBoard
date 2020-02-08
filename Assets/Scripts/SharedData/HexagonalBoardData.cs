using System.Collections.Generic;
using HexCardGame.Runtime;
using UnityEngine;

namespace HexCardGame.SharedData
{
    public abstract class BoardData : ScriptableObject
    {
        public abstract Hex[] GetHexPositions();
    }

    [CreateAssetMenu(menuName = "Data/BoardData", fileName = "HexagonalBoardData")]
    public class HexagonalBoardData : BoardData
    {
        [Range(1, 10)] public int radius;
        
        public override Hex[] GetHexPositions()
        {
            var positions = new List<Hex>();
            // radius = 1
            // -1, 0, 1
            //
            
//           positions.Add(new Hex(-1, -1));
            for (var x = -radius; x <= radius; x++)
            {
                var yMin = Mathf.Max(-radius, -x -radius);//-1
                var yMax = Mathf.Min(radius, -x +radius); // 1
                for (var y = yMin; y <= yMax; y++) 
                    positions.Add(new Hex(x, y));
            }

            return positions.ToArray();
        }
    }
}