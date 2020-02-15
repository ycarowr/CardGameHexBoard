using UnityEngine;

namespace HexBoardGame.Runtime
{
    /// <summary>
    ///     Interface that manipulates a board shape.
    /// </summary>
    public interface IBoardManipulation
    {
        bool Contains(Vector3Int cell);
        Vector3Int[] GetNeighbours(Vector3Int cell);
        Vector3Int[] GetVertical(Vector3Int cell, int length);
        Vector3Int[] GetHorizontal(Vector3Int cell, int length);
        Vector3Int[] GetDiagonalAscendant(Vector3Int cell, int length);
        Vector3Int[] GetDiagonalDescendant(Vector3Int cell, int length);

        //TODO:
        //1. Range
        //2. Path finding
        //3. More useful methods...
    }
}