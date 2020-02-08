using HexCardGame.Runtime.GameBoard;
using UnityEngine;

namespace HexCardGame.Runtime
{
    public static class HexHelper
    {
        public static Hex Add(Hex a, Hex b) => new Hex(a.x + b.x, a.y + b.y);
        public static Hex Subtract(Hex a, Hex b) => new Hex(a.x - b.x, a.y - b.y);
        public static Hex Multiply(Hex a, int k) => new Hex(a.x * k, a.y * k);
        public static int Distance(Hex a, Hex b) => Subtract(a, b).Length;

        public static Vector3Int CubeToOddr(Hex hex)
        {
            var col = hex.x + (hex.z - (hex.z & 1)) / 2;
            var row = hex.z;
            return new Vector3Int(col, row, 0);
        }

        public static Hex OddrToCube(Vector3Int pos)
        {
            var x = pos.x - (pos.x - (pos.y & 1)) / 2;
            return new Hex(x, pos.y);
        }
    }
}