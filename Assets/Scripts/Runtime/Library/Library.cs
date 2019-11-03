using System.Collections.Generic;
using HexCardGame.SharedData;
using Tools;
using Tools.Extensions.Arrays;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateLibrary
    {
        void OnCreateLibrary(ILibrary library);
    }

    public interface ILibrary
    {
        int Size { get; }
        CardData GetRandomData();
        CardData GetRandomDataFromPlayer(PlayerId id);
    }

    public class Library : ILibrary
    {
        readonly CardData[] _register;
        readonly Dictionary<PlayerId, CardData[]> _registerByPlayer;

        public Library(Dictionary<PlayerId, CardData[]> playersLibrary, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            _registerByPlayer = playersLibrary;

            var size = 0;
            foreach (var value in _registerByPlayer.Values)
                size += value.Length;

            _register = new CardData[size];
            var count = 0;
            foreach (var cardDatas in _registerByPlayer.Values)
            foreach (var i in cardDatas)
            {
                _register[count] = i;
                ++count;
            }

            OnCreateLibrary();
        }

        IDispatcher Dispatcher { get; }
        public int Size => _register.Length;
        public CardData GetRandomData() => _register.RandomItem();
        public CardData GetRandomDataFromPlayer(PlayerId id) => _registerByPlayer[id].RandomItem();

        void OnCreateLibrary()
        {
            Logger.Log<Library>("Runtime Library Dispatched");
            Dispatcher.Notify<ICreateLibrary>(i => i.OnCreateLibrary(this));
        }
    }
}