using System.Collections.Generic;
using Tools.GenericCollection;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.Model.GamePool
{
    public class Pool : Collection<Position>
    {
        EventsDispatcher Dispatcher { get; }
        
        public Pool(GameParameters configs, EventsDispatcher dispatcher)
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
        
        void OnCreatePool()
        {
            Logger.Log<Pool>("Pool Model Dispatched");
            Dispatcher.Notify<ICreatePool>(i => i.OnCreatePool(this));
        }
    }
}