using HexCardGame.Runtime.GameBoard;
using UnityEngine;
using UnityEngine.Assertions;

namespace HexCardGame.Runtime
{
    public struct Hex
    {
        public int x { get; } //column, X axis
        public int y { get; } //row, Y axis
        public int z { get; }

        public Hex(int x, int y)
        {
            this.x = x;
            this.y = y;
            z = -(x + y);
            Assert.AreEqual(z + x + y, 0);
        }
        
        public int Length => (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z)) / 2;
        public static bool operator == (Hex a, Hex b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator != (Hex a, Hex b) => !(a == b);
    }
}