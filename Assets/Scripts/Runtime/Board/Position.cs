using UnityEngine;

namespace HexCardGame.Runtime.GameBoard
{
    public class Position<T> : IDataStorage<T> where T : class
    {
        public Hex Hex { get; }
        public int x => Hex.x;
        public int y => Hex.y;
        public Position(Hex hex) => Hex = hex;
        public bool HasData => Data != null;
        public T Data { get; private set; }
        public void SetData(T value) => Data = value;
        public static Vector3Int AsVector3Int(Position<T> p) => new Vector3Int(p.x, p.y, 0);
        static Vector2Int AsVector2Int(Position<T> p) => new Vector2Int(p.x, p.y);

        public static bool AreEqual(Position<T> p1, Position<T> p2)
        {
            if (p1 == null && p2 == null)
                return true;

            if (p1 == null || p2 == null)
                return false;

            return p1.IsEqual(p2);
        }

        public static implicit operator Vector3Int(Position<T> p) => AsVector3Int(p);
        public static implicit operator Vector2Int(Position<T> p) => AsVector2Int(p);
        public override string ToString() => $"Position: {x},{y}";

        public bool IsEqual(Position<T> p)
        {
            if (p == null)
                return false;
            if (p.x != x)
                return false;
            return p.y == y;
        }
    }
}