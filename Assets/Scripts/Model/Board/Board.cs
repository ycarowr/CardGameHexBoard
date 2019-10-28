using System.Collections.Generic;
using Tools.GenericCollection;
using UnityEngine;

namespace HexCardGame
{
    public class Board : Collection<Position>
    {
        EventsDispatcher Dispatcher { get; }
        public int MaxX { get; }
        public int MaxY { get; }
        
        public Board(GameParameters configs, EventsDispatcher dispatcher)
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
            Debug.Log($"Model Added position {x}, {y}");
            var position = new Position(x, y);
            Add(position);
        }

        public Position Get(int x, int y) => Units.Find(p => p.X == x && p.Y == y);

        void OnCreateBoard() => Dispatcher.Notify<ICreateBoard>(i => i.OnCreateBoard(this));
    }
}