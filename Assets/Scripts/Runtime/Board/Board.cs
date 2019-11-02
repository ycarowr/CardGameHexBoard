using HexCardGame.SharedData;
using Tools;
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
        Position[] Positions { get; }
        bool Has(int x, int y);
        Position Get(int x, int y);
        void GeneratePositions();
        void Clear();
    }

    public class Board : IBoard
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
        public Position[] Positions { get; private set; }

        public void GeneratePositions()
        {
            var desiredPositions = Data.GetDesiredPositions();
            Positions = new Position[desiredPositions.Length];
            for (var i = 0; i < desiredPositions.Length; i++)
                Positions[i] = new Position(desiredPositions[i].x, desiredPositions[i].y);

            OnCreateBoard();
        }

        public bool Has(int x, int y) => Get(x, y) != null;

        public Position Get(int x, int y)
        {
            foreach (var i in Positions)
            {
                if (i.X != x)
                    continue;
                if (i.Y == y)
                    return i;
            }

            return null;
        }

        public void Clear() => GeneratePositions();

        void OnCreateBoard()
        {
            Logger.Log<Board>("Runtime Board Dispatched");
            Dispatcher.Notify<ICreateBoard>(i => i.OnCreateBoard(this));
        }
    }
}