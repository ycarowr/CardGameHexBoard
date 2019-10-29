using System.Collections;
using System.Collections.Generic;
using Tools.Extensions.List;
using Tools.GenericCollection;
using Tools.Patterns.Observer;
using UnityEngine;
using Logger = Tools.Logger;


namespace HexCardGame.Model
{
    [Event]
    public interface ICreateLibrary
    {
        void OnCreateLibrary(ILibrary library);
    }
    
    public interface ILibrary
    {
        void GenerateCardFromRandomData();
        void GenerateCardFromPlayerData(PlayerId id);
    }
    
    public class Library : Collection<object>, ILibrary
    {
        IDispatcher Dispatcher { get; }
        readonly List<object> _library = new List<object>();
        readonly Dictionary<PlayerId, List<object>> _libraryByPlayer;
        
        public Library(Dictionary<PlayerId, List<object>> playersLibrary, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            _libraryByPlayer = playersLibrary;
            foreach (var key in _libraryByPlayer.Keys)
                _library.AddRange(_libraryByPlayer[key]);
            OnCreateLibrary();
        }

        public void GenerateCardFromRandomData() => _library.RandomItem();
        public void GenerateCardFromPlayerData(PlayerId id) => _libraryByPlayer[id].RandomItem();
        void OnCreateLibrary()
        {
            Logger.Log<Library>("Create Library Model");
            Dispatcher.Notify<ICreateLibrary>(i => i.OnCreateLibrary(this));
        }
    }
}