using UnityEngine;

namespace HexCardGame.Model.GameBoard
{
    public static class BoardHexUtility
    {
        public static Vector3Int AsVector3Int(this Position p) => new Vector3Int(p.X, p.Y, 0);
        public static Vector2Int AsVector2Int(this Position p) => new Vector2Int(p.X, p.Y);
    }

    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        int Z => -(X + Y);
        public static implicit operator Vector3Int(Position p) => p.AsVector3Int();
        public static implicit operator Vector2Int(Position p) => p.AsVector2Int();
        public override string ToString() => $"Position: {X},{Y}";

        public bool AreEqual(Position p)
        {
            if (p == null)
                return false;
            if (p.X != X)
                return false;
            return p.Y == Y;
        }
    }
}