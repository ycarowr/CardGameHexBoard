using System.Collections.Generic;
using Tools.GenericCollection;
using Tools.Patterns.Observer;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.Model.GamePool
{
    [Event]
    public interface ICreatePool
    {
        void OnCreatePool(IPool pool);
    }
    
    public interface IPool
    {
        int Size { get; }
        void AddCardAtPosition(object card, PoolPositionId id);
    }
    
    public class Pool : Collection<Position>, IPool
    {
        IDispatcher Dispatcher { get; }
        
        public Pool(GameParametersReference configs, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;

            foreach (var id in Position.GetAllIds())
                AddPosition(id);
            OnCreatePool();
        }

        void AddPosition(PoolPositionId id)
        {
            var position = new Position(id);
            Add(position);
        }

        Position Get(PoolPositionId id) => Units.Find(x => x.Id == id);
        public void AddCardAtPosition(object card, PoolPositionId id)
        {
            var position = Get(id);
            if(!position.HasCard) 
                position.SetStoredCard(card);
        }

        void OnCreatePool()
        {
            Logger.Log<Pool>("Pool Model Dispatched");
            Dispatcher.Notify<ICreatePool>(i => i.OnCreatePool(this));
        }

    }
}