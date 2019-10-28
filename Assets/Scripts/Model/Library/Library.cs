using System.Collections;
using System.Collections.Generic;
using Tools.Extensions.List;
using Tools.GenericCollection;
using UnityEngine;
using Logger = Tools.Logger;


namespace HexCardGame.Model
{
    public class Library : Collection<object>
    {
        EventsDispatcher Dispatcher { get; }
        readonly List<object> _library = new List<object>();
        readonly Dictionary<PlayerId, List<object>> _libraryByPlayer;
        
        public Library(Dictionary<PlayerId, List<object>> playersLibrary, EventsDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            _libraryByPlayer = playersLibrary;
            foreach (var key in _libraryByPlayer.Keys)
                _library.AddRange(_libraryByPlayer[key]);
            OnCreateLibrary();
        }

        public void GenerateFromRandomData() => _library.RandomItem();
        public void GenerateFromPlayer(PlayerId id) => _libraryByPlayer[id].RandomItem();
        void OnCreateLibrary()
        {
            Logger.Log<Library>("Create Library Model");
            Dispatcher.Notify<ICreateLibrary>(i => i.OnCreateLibrary(this));
        }
    }
}