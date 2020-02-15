using HexBoardGame.Runtime;
using HexCardGame.SharedData;
using Tools.Extensions.Arrays;
using Tools.Patterns.Observer;
using UnityEngine;

namespace HexCardGame.Runtime.GameBoard
{
    [Event]
    public interface ICreateBoard<T> where T : class
    {
        void OnCreateBoard(IBoard<T> board);
    }

    public interface IBoard<T> : IBoardDataStorage<T> where T : class
    {
        Position<T>[] Positions { get; }
        bool HasPosition(int x, int y);
        Position<T> GetPosition(int x, int y);
        Position<T> GetPosition(Vector3Int position);
        void GeneratePositions();
    }

    public partial class Board<T> : IBoard<T> where T : class
    {
        public Board(GameParameters parameters, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            Data = parameters.BoardData;
            GeneratePositions();
        }

        IDispatcher Dispatcher { get; }
        public BoardData Data { get; }
        public Position<T>[] Positions { get; private set; }

        public void GeneratePositions()
        {
            var hexPoints = Data.GetHexPoints();
            var cellPoints = BoardManipulationOddR.ConvertGroup(hexPoints);
            cellPoints.Print("CellPoints: ");
            Positions = new Position<T>[cellPoints.Length];
            for (var index = 0; index < cellPoints.Length; index++)
            {
                var i = cellPoints[index];
                Positions[index] = new Position<T>(i);
            }

            foreach (var i in Positions)
            {
                Debug.Log("Generated position: "+ i.Cell);
            }
            OnCreateBoard();
        }

        public bool HasPosition(int x, int y) => GetPosition(x, y) != null;
        public Position<T> GetPosition(Vector3Int p) => GetPosition(p.x, p.y);

        public Position<T> GetPosition(int x, int y)
        {
            foreach (var i in Positions)
            {
                if (i.x != x)
                    continue;
                if (i.y == y)
                    return i;
            }

            return null;
        }

        public void Clear() => GeneratePositions();

        void OnCreateBoard() => Dispatcher.Notify<ICreateBoard<T>>(i => i.OnCreateBoard(this));
    }
}