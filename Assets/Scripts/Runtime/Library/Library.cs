using System.Collections.Generic;
using HexCardGame.SharedData;
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
        CardData GetRandomDataFromPlayer(SeatType id);
    }

    public class Library : ILibrary
    {
        private readonly CardData[] _register;
        private readonly Dictionary<SeatType, CardData[]> _registerByPlayer;

        public Library(Dictionary<SeatType, CardData[]> playersLibrary, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            _registerByPlayer = playersLibrary;

            var count = 0;
            var size = 0;

            foreach (var value in _registerByPlayer.Values)
                size += value.Length;

            _register = new CardData[size];
            foreach (var data in _registerByPlayer.Values)
            foreach (var i in data)
            {
                _register[count] = i;
                ++count;
            }

            OnCreateLibrary();
        }

        private IDispatcher Dispatcher { get; }
        public int Size => _register.Length;

        public CardData GetRandomData()
        {
            return _register.RandomItem();
        }

        public CardData GetRandomDataFromPlayer(SeatType id)
        {
            return _registerByPlayer[id].RandomItem();
        }

        private void OnCreateLibrary()
        {
            Dispatcher.Notify<ICreateLibrary>(i => i.OnCreateLibrary(this));
        }
    }
}