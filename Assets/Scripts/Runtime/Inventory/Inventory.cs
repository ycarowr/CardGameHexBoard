using System.Collections.Generic;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateInventory
    {
        void OnCreateInventory(IInventory inventory);
    }

    [Event]
    public interface IAddItem
    {
        void OnAddItem(PlayerId playerId, IItem item, int total, int amount);
    }

    [Event]
    public interface IRemoveItem
    {
        void OnRemoveItem(PlayerId playerId, IItem item, int total, int amount);
    }


    public class Inventory : IInventory
    {
        public static readonly Gold GoldItem = new Gold();
        public static readonly ActionPoint ActionPointItem = new ActionPoint();

        readonly Dictionary<string, ItemEntry> _register = new Dictionary<string, ItemEntry>();

        public Inventory(PlayerId id, GameParameters parameters, IDispatcher dispatcher)
        {
            Id = id;
            Parameters = parameters;
            Dispatcher = dispatcher;
            OnCreateInventory();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }

        public PlayerId Id { get; }

        void OnCreateInventory() => Dispatcher.Notify<ICreateInventory>(i => i.OnCreateInventory(this));

        class ItemEntry
        {
            public int Amount;
            public IItem Item;
        }

        #region Operations

        public int GetAmount(IItem item)
        {
            var id = item.ItemId;
            var hasItem = HasItem(item, 1);
            return !hasItem ? 0 : _register[id].Amount;
        }

        public bool HasItem(IItem item, int amount)
        {
            var id = item.ItemId;
            if (!_register.ContainsKey(id))
                return false;

            return _register[id].Amount >= amount;
        }

        public IItem GetItem(string id) => _register[id].Item;

        public void AddItem(IItem item, int amount)
        {
            var contains = _register.ContainsKey(item.ItemId);
            if (contains)
                _register[item.ItemId].Amount += amount;
            else
                _register.Add(item.ItemId, new ItemEntry
                {
                    Item = item,
                    Amount = amount
                });

            var total = _register[item.ItemId].Amount;
            OnAddItem(total, item, amount);
        }


        public void RemoveItem(IItem item, int amount)
        {
            var id = item.ItemId;
            var contains = _register.ContainsKey(id);
            if (!contains)
                return;

            var total = _register[id].Amount - amount;
            if (total > 0)
                _register[id].Amount = total;
            else
                _register.Remove(id);
            OnRemoveItem(total, item, amount);
        }

        void OnAddItem(int total, IItem item, int amount) =>
            Dispatcher.Notify<IAddItem>(i => i.OnAddItem(Id, item, total, amount));

        void OnRemoveItem(int total, IItem item, int amount) =>
            Dispatcher.Notify<IRemoveItem>(i => i.OnRemoveItem(Id, item, total, amount));

        #endregion
    }
}