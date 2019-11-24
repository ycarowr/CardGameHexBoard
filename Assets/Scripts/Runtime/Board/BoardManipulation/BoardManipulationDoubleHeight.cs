using HexCardGame.Runtime.GameBoard;
using UnityEngine;

namespace HexCardGame.Runtime
{
    public class BoardManipulationDoubleHeight<T> : IBoardManipulation<T> where T : class
    {
        readonly Vector3Int[][] _oddrDirections;

        public BoardManipulationDoubleHeight(IBoard<T> board)
        {
            Board = board;
            _oddrDirections = new Vector3Int[2][];
            _oddrDirections[0] = new[]
            {
                new Vector3Int(+1, 0, 0),
                new Vector3Int(0, -1, 0),
                new Vector3Int(-1, -1, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(-1, +1, 0),
                new Vector3Int(0, +1, 0)
            };
            _oddrDirections[1] = new[]
            {
                new Vector3Int(+1, 0, 0),
                new Vector3Int(+1, -1, 0),
                new Vector3Int(0, -1, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(0, +1, 0),
                new Vector3Int(+1, +1, 0)
            };
        }

        IBoard<T> Board { get; }
        int MaxX => Board.MaxX;
        int MaxY => Board.MaxY;

        public Vector3Int[] GetNeighbours(int x, int y)
        {
            var parity = y & 1;
            var currentDirection = _oddrDirections[parity];
            var neighbors = new Vector3Int[] { };
            foreach (var direction in currentDirection)
                neighbors = neighbors.Merge(Get(direction.x + x, direction.y + y));
            return neighbors;
        }

        public Vector3Int[] Get(int x, int y) => new[] {new Vector3Int(x, y, 0)};
        public Vector3Int[] GetVertical(Vector3Int direction) => GetVertical(direction, 10);

        public Vector3Int[] GetHorizontal(Vector3Int direction) => new Vector3Int[1];
        public Vector3Int[] GetDiagonalAscendant(Vector3Int direction) => GetAllDiagonalAscendant(direction, 10);
        public Vector3Int[] GetDiagonalDescendent(Vector3Int direction) => GetAllDiagonalDescendant(direction, 10);


        #region Sequence

        public Vector3Int[] GetVertical(Vector3Int direction, int n)
        {
            var vertical = new Vector3Int[n];
//            if (n > 0)
//            {
//                for (var i = 0; i <= n; i++)
//                    vertical = vertical.Merge(Get(x + i, y));
//            }
//            else
//            {
//                for (var i = 0; i <= -n; i++)
//                    vertical = vertical.Merge(Get(x - i, y));
//            }

            return vertical;
        }

        public Vector3Int[] GetAllDiagonalDescendant(Vector3Int direction, int n)
        {
            var diagDescendant = new Vector3Int[n];

//            if (n > 0)
//            {
//                var max = Mathf.Min(n, MaxX);
//                for (var i = 1; i <= max; i++)
//                    diagDescendant = diagDescendant.Merge(Get(x - i, y + i));
//            }
//            else
//            {
//                var max = Mathf.Min(x - n, MaxX);
//                for (var i = 0; i < max; i++)
//                    diagDescendant = diagDescendant.Merge(Get(x + i, y - i));
//            }

            return diagDescendant;
        }

        public Vector3Int[] GetAllDiagonalAscendant(Vector3Int direction, int n)
        {
            var diagAscendant = new Vector3Int[n];

//            if (n > 0)
//            {
//                var max = Mathf.Min(y + n + 1, MaxX);
//                for (var i = y; i < max; i++)
//                {
//                    if (i != y)
//                        diagAscendant = diagAscendant.Merge(Get(x, i));
//                }
//            }
//            else
//            {
//                var min = Mathf.Max(y + n, 0);
//                for (var i = y; i >= min; i--)
//                {
//                    if (i != y)
//                        diagAscendant = diagAscendant.Merge(Get(x, i));
//                }
//            }

            return diagAscendant;
        }

        #endregion
    }
}