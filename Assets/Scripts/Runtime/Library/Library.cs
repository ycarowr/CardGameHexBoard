using System.Collections.Generic;
using HexCardGame.SharedData;
using Tools;
using Tools.Extensions.List;
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
        CardData GetRandomData();
        CardData GetRandomDataFromPlayer(PlayerId id);
    }

    public class Library : ILibrary
    {
        readonly List<CardData> _library = new List<CardData>();
        readonly Dictionary<PlayerId, List<CardData>> _libraryByPlayer;

        public Library(Dictionary<PlayerId, List<CardData>> playersLibrary, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            _libraryByPlayer = playersLibrary;
            foreach (var key in _libraryByPlayer.Keys)
                _library.AddRange(_libraryByPlayer[key]);
            OnCreateLibrary();
        }

        IDispatcher Dispatcher { get; }
        public CardData GetRandomData() => _library.RandomItem();
        public CardData GetRandomDataFromPlayer(PlayerId id) => _libraryByPlayer[id].RandomItem();

        void OnCreateLibrary()
        {
            Logger.Log<Library>("Runtime Library Dispatched");
            Dispatcher.Notify<ICreateLibrary>(i => i.OnCreateLibrary(this));
        }
    }
}