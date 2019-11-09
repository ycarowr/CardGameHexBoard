using HexCardGame.SharedData;
using Tools.Patterns.Observer;
using UnityEngine;
using Logger = Tools.Logger;

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
        public int MaxX => Data.MaxX;
        public int MaxY => Data.MaxY;
        public BoardData Data { get; }
        public Position<T>[] Positions { get; private set; }

        public void GeneratePositions()
        {
            var desiredPositions = Data.GetDesiredPositions();
            Positions = new Position<T>[desiredPositions.Length];
            for (var i = 0; i < desiredPositions.Length; i++)
                Positions[i] = new Position<T>(desiredPositions[i].x, desiredPositions[i].y);

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

        void OnCreateBoard()
        {
            Logger.Log<Board<T>>("Runtime Board Dispatched");
            Dispatcher.Notify<ICreateBoard<T>>(i => i.OnCreateBoard(this));
        }
    }
}