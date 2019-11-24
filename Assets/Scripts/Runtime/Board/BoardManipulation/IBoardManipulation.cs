using UnityEngine;

namespace HexCardGame.Runtime
{
    public interface IBoardManipulation<T> where T : class
    {
        Vector3Int[] GetNeighbours(int x, int y);
//        Vector3Int[] Get(int x, int y);
//        Vector3Int[] GetVertical(int x, int y);
//        Vector3Int[] GetHorizontal(int x, int y);
//        Vector3Int[] GetDiagonalAscendant(int x, int y);
//        Vector3Int[] GetDiagonalDescendent(int x, int y);
    }
}