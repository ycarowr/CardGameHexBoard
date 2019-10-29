using System.Collections.Generic;
using HexCardGame.SharedData;
using Tools;
using Tools.GenericCollection;
using Tools.Patterns.Observer;

namespace HexCardGame.Model.GameBoard
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
                AddPosition(i, j);
            RemoveUndesiredPositions();
            OnCreateBoard();
        }

        public Position Get(int x, int y) => Units.Find(p => p.X == x && p.Y == y);

        void RemoveUndesiredPositions()
        {
            foreach (var i in Data.UndesiredPositions)
                Remove(Get(i.x, i.y));
        }

        void AddPosition(int x, int y) => Add(new Position(x, y));

        void OnCreateBoard()
        {
            Logger.Log<Board>("Board Model Dispatched");
            Dispatcher.Notify<ICreateBoard>(i => i.OnCreateBoard(this));
        }
    }
}