using System.Collections;
using System.Collections.Generic;
using Tools.Patterns.Observer;
using UnityEngine;

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
        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }
        public Score(IPlayer[] players, GameParameters parameters, IDispatcher dispatcher)
        {
            Parameters = parameters;
            Dispatcher = dispatcher;
            foreach (var i in players)
                RegisterPlayer(i.Id);
            OnCreateScore();
        }

        void RegisterPlayer(PlayerId id) => _register.Add(id, 0);
        public void Add(PlayerId id, int amount) => _register[id] += amount;
        public void Remove(PlayerId id, int amount) => _register[id] -= amount;
        public int GetScoreForPlayer(PlayerId id) => _register[id];
        public void Clear() => _register.Clear();

        void OnCreateScore() => Dispatcher.Notify<ICreateScore>(i => i.OnCreateScore(this));
    }
}