using System.Collections.Generic;
using Tools.GenericCollection;
using Tools;
using Tools.Patterns.Observer;
using UnityEngine;

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
    }
    
    public class Board : Collection<Position>, IBoard
    {
        IDispatcher Dispatcher { get; }
        public int MaxX { get; }
        public int MaxY { get; }
        public List<Position> Positions => Units;
        
        public Board(GameParametersReference configs, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            MaxX = 6;
            MaxY = 6;
            for (int i = 0; i < MaxX; i++)
            {
                for (int j = 0; j < MaxY; j++)
                {
                    AddPosition(i, j);
                }
            }

            RemoveUndesiredPositions();
            OnCreateBoard();
        }

        void RemoveUndesiredPositions()
        {
            
        }

        void AddPosition(int x, int y)
        {
            var position = new Position(x, y);
            Add(position);
        }

        public Position Get(int x, int y) => Units.Find(p => p.X == x && p.Y == y);

        void OnCreateBoard()
        {
            Tools.Logger.Log<Board>("Board Model Dispatched");
            Dispatcher.Notify<ICreateBoard>(i => i.OnCreateBoard(this));
        }
    }
}