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
        int GetScoreForPlayer(PlayerId id);
        void Add(PlayerId id, int amount);
        void Remove(PlayerId id, int amount);
        void Clear();
    }

    public class Score : IScore
    {
        readonly Dictionary<PlayerId, int> _register = new Dictionary<PlayerId, int>();

        public Score(IPlayer[] players, GameParameters parameters, IDispatcher dispatcher)
        {
            Parameters = parameters;
            Dispatcher = dispatcher;
            foreach (var i in players)
                RegisterPlayer(i.Id);
            OnCreateScore();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }
        public void Add(PlayerId id, int amount) => _register[id] += amount;
        public void Remove(PlayerId id, int amount) => _register[id] -= amount;
        public int GetScoreForPlayer(PlayerId id) => _register[id];

        public void Clear()
        {
            var keys = _register.Keys.ToArray();
            _register.Clear();
            foreach (var id in keys)
                RegisterPlayer(id);
        }

        void RegisterPlayer(PlayerId id) => _register.Add(id, 0);

        void OnCreateScore() => Dispatcher.Notify<ICreateScore>(i => i.OnCreateScore(this));
    }
}