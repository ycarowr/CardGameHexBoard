using System.Collections.Generic;
using HexCardGame.SharedData;
using Tools;
using Tools.GenericCollection;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GameBoard
{
    [Event]
    public interface ICreateBoard
    {
        void OnCreateBoard(IBoard board);
    }

    public interface IBoard
    {
        List<Position> Positions { get; }
        Position Get(int x, int y);
        void GeneratePositions();
        void Clear();
    }

    public class Board : Collection<Position>, IBoard
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
        public List<Position> Positions => Units;

        public void GeneratePositions()
        {
            for (var i = 0; i < MaxX; i++)
            for (var j = 0; j < MaxY; j++)
                Add(new Position(i, j));
            RemoveUndesiredPositions();
            OnCreateBoard();
        }

        public Position Get(int x, int y)
        {
            foreach (var i in Units)
            {
                if (i.X != x)
                    continue;
                if (i.Y == y)
                    return i;
            }

            return null;
        }

        void RemoveUndesiredPositions()
        {
            foreach (var i in Data.UndesiredPositions)
                Remove(Get(i.x, i.y));
        }

        void OnCreateBoard()
        {
            Logger.Log<Board>("Runtime Board Dispatched");
            Dispatcher.Notify<ICreateBoard>(i => i.OnCreateBoard(this));
        }
    }
}