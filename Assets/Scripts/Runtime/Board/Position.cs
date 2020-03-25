using UnityEngine;

namespace HexCardGame.Runtime.GameBoard
{
    public class Position<T> : IDataStorage<T> where T : class
    {
        public Position(Vector3Int cell)
        {
            Cell = cell;
        }

        public int y => Cell.y;
        public int x => Cell.x;
        public Vector3Int Cell { get; }
        public bool HasData => Data != null;
        public T Data { get; private set; }

        public void SetData(T value)
        {
            Data = value;
        }

        public override string ToString()
        {
            return $"Position: {x},{y}";
        }

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