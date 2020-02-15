
using HexCardGame.SharedData;
using Tools.Extensions.Arrays;
using UnityEngine;

namespace HexBoardGame.Runtime
{
    /// <summary>
    ///     The way to manipulate a board in the Odd-Row layout.
    /// </summary>
    public class BoardManipulationOddR : IBoardManipulation
    {
        static readonly Hex[] NeighboursDirections =
        {
            new Hex(1, 0), new Hex(1, -1), new Hex(0, -1),
            new Hex(-1, 0), new Hex(-1, 1), new Hex(0, 1)
        };

        readonly Hex[] _hexPoints;

        public BoardManipulationOddR(BoardData dataShape) => _hexPoints = dataShape.GetHexPoints();

        public Vector3Int[] GetNeighbours(Vector3Int cell)
        {
            var point = ConvertHexCoordinate(cell);
            var center = GetIfExistsOrEmpty(point);
            var neighbours = new Hex[] { };
            foreach (var direction in NeighboursDirections)
            {
                var neighbour = Hex.Add(center[0], direction);
                var array = new[] {neighbour};
                neighbours = neighbours.Append(array);
            }
            
            return ConvertGroup(neighbours);
        }

        /// <summary>
        ///     If the point is present among the starting configuration returns it. Otherwise returns a empty array.
        /// </summary>
        Hex[] GetIfExistsOrEmpty(Hex hex)
        {
            foreach (var i in _hexPoints)
                if (i == hex)
                    return new[] {i};

            return new Hex[] { };
        }

        #region Operations

        public bool Contains(Vector3Int cell)
        {
            var center = ConvertHexCoordinate(cell);
            return GetIfExistsOrEmpty(center).Length > 0;
        }

        public Vector3Int[] GetVertical(Vector3Int cell, int length) => new Vector3Int[] { };

        public Vector3Int[] GetHorizontal(Vector3Int cell, int length)
        {
            var center = ConvertHexCoordinate(cell);
            var halfLength = length / 2;
            var points = GetIfExistsOrEmpty(center);
            var x = center.q;
            var y = center.r;

            for (var i = 1; i <= halfLength; i++)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x + i, y)));

            for (var i = -1; i >= -halfLength; i--)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x + i, y)));
            
            return ConvertGroup(points);
        }

        public Vector3Int[] GetDiagonalAscendant(Vector3Int cell, int length)
        {
            var center = ConvertHexCoordinate(cell);
            var halfLength = length / 2;
            var points = GetIfExistsOrEmpty(center);
            var x = center.q;
            var y = center.r;

            for (var i = 1; i <= halfLength; i++)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x, y + i)));

            for (var i = -1; i >= -halfLength; i--)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x, y + i)));

            return ConvertGroup(points);
        }

        public Vector3Int[] GetDiagonalDescendant(Vector3Int cell, int length)
        {
            var center = ConvertHexCoordinate(cell);
            var halfLength = length / 2;
            var points = GetIfExistsOrEmpty(center);
            var x = center.q;
            var y = center.r;

            for (var i = 1; i <= halfLength; i++)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x - i, y + i)));

            for (var i = -1; i >= -halfLength; i--)
                points = points.Append(GetIfExistsOrEmpty(new Hex(x - i, y + i)));

            return ConvertGroup(points);
        }

        public static Hex[] ConvertGroup(params Vector3Int[] input)
        {
            var output = new Hex[input.Length];
            for (var i = 0; i < input.Length; i++) 
                output[i] = ConvertHexCoordinate(input[i]);
            return output;
        }
        
        public static Vector3Int[] ConvertGroup(params Hex[] input)
        {
            var output = new Vector3Int[input.Length];
            for (var i = 0; i < input.Length; i++) 
                output[i] = ConvertCellCoordinate(input[i]);
            
            input.Print(" Input: ---------------------- ");
            output.Print(" Output: ---------------------- ");
            return output;
        }

        public static Hex ConvertHexCoordinate(Vector3Int cell) =>
            OffsetCoordHelper.RoffsetToCube(OffsetCoord.Parity.Odd, new OffsetCoord(cell.x, cell.y));

        public static Vector3Int ConvertCellCoordinate(Hex hex) =>
            OffsetCoordHelper.RoffsetFromCube(OffsetCoord.Parity.Odd, hex).ToVector3Int();

        #endregion
    }
}