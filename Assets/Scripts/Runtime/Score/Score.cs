using System.Collections.Generic;
using System.Linq;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GameScore
{
    [Event]
    public interface ICreateScore
    {
        void OnCreateScore(IScore score);
    }

    public interface IScore
    {
        int GetScoreForPlayer(SeatType id);
        void Add(SeatType id, int amount);
        void Remove(SeatType id, int amount);
        void Clear();
    }

    public class Score : IScore
    {
        readonly Dictionary<SeatType, int> _register = new Dictionary<SeatType, int>();

        public Score(IPlayer[] players, GameParameters parameters, IDispatcher dispatcher)
        {
            Parameters = parameters;
            Dispatcher = dispatcher;
            foreach (var i in players)
                RegisterPlayer(i.Seat);
            OnCreateScore();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }
        public void Add(SeatType id, int amount) => _register[id] += amount;
        public void Remove(SeatType id, int amount) => _register[id] -= amount;
        public int GetScoreForPlayer(SeatType id) => _register[id];

        public void Clear()
        {
            var keys = _register.Keys.ToArray();
            _register.Clear();
            foreach (var id in keys)
                RegisterPlayer(id);
        }

        void RegisterPlayer(SeatType id) => _register.Add(id, 0);

        void OnCreateScore() => Dispatcher.Notify<ICreateScore>(i => i.OnCreateScore(this));
    }
}