using System.Collections;
using System.Collections.Generic;
using Tools.Patterns.Observer;
using UnityEngine;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateInventory
    {
        void OnCreateInventory(IInventory inventory);
    }

    [Event]
    public interface IAddGold
    {
        void OnAddGold(PlayerId playerId, int total, int amount);
    }
    
    [Event]
    public interface IRemoveGold
    {
        void OnRemoveGold(PlayerId playerId, int total, int amount);
    }

    public class Inventory : IInventory
    {
        class ItemEntry
        {
            public IItem Item;
            public int Amount;
        }

        readonly Dictionary<string, ItemEntry> _register = new Dictionary<string, ItemEntry>();
        public static readonly Gold GoldItem = new Gold();

        public PlayerId Id { get; }
        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }

        public Inventory(PlayerId id, GameParameters parameters, IDispatcher dispatcher)
        {
            Id = id;
            Parameters = parameters;
            Dispatcher = dispatcher;
            OnCreateInventory();
            AddItem(GoldItem, Parameters.StartingGold);
        }

        void OnCreateInventory() => Dispatcher.Notify<ICreateInventory>(i => i.OnCreateInventory(this));

        #region Operations

        public int GetAmount(string id)
        {
            var hasItem = HasItem(id, 1);
            return !hasItem ? 0 : _register[id].Amount;
        }

        public bool HasItem(string id, int amount)
        {
            if (!_register.ContainsKey(id))
                return false;

            return _register[id].Amount >= amount;
        }

        public IItem GetItem(string id) => _register[id].Item;

        public void AddItem(IItem item, int amount)
        {
            var contains = _register.ContainsKey(item.ProductId);
            if (contains)
                _register[item.ProductId].Amount += amount;
            else
            {
                _register.Add(item.ProductId, new ItemEntry()
                {
                    Item = item,
                    Amount = amount
                });
            }

            var total = _register[item.ProductId].Amount;
            OnAddGold(total, amount);
        }


        public void RemoveItem(string id, int amount)
        {
            var contains = _register.ContainsKey(id);
            if (!contains)
                return;

            var total = _register[id].Amount - amount;
            if (total > 0)
                _register[id].Amount = total;
            else
                _register.Remove(id);

            OnRemoveGold(total, amount);
        }

        void OnAddGold(int total, int amount) => Dispatcher.Notify<IAddGold>(i => i.OnAddGold(Id, total, amount));
        void OnRemoveGold(int total, int amount) => Dispatcher.Notify<IRemoveGold>(i => i.OnRemoveGold(Id, total, amount));

        #endregion
    }
}