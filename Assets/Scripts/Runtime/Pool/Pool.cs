using System;
using Tools;
using Tools.GenericCollection;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GamePool
{
    [Event]
    public interface ICreatePool
    {
        void OnCreatePool(IPool pool);
    }

    public interface IPool
    {
        int Count { get; }
        void AddCardAt(CardPool card, PoolPositionIndex index);
        void RemoveCardAt(PoolPositionIndex index);
        CardPool GetCardAt(PoolPositionIndex index);
        CardPool GetAndRemoveCardAt(PoolPositionIndex index);
        void FlipCardAt(PoolPositionIndex index);
        void Clear();
    }

    public class Pool : Collection<Position>, IPool
    {
        public Pool(GameParameters gameParams, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            CreatePool();
        }

        IDispatcher Dispatcher { get; }

        public void FlipCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            card?.SetFaceUp(!card.IsFaceUp);
        }

        public CardPool GetCardAt(PoolPositionIndex index) => Position(index)?.Value;

        public CardPool GetAndRemoveCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            RemoveCardAt(index);
            return card;
        }

        public void AddCardAt(CardPool card, PoolPositionIndex index) => Position(index)?.SetValue(card);
        public void RemoveCardAt(PoolPositionIndex index) => Position(index)?.SetValue(null);

        Position Position(PoolPositionIndex index) => Units[(int) index];

        void CreatePool()
        {
            var count = PoolPositionUtility.GetAllIdsInt().Length;
            for (var i = 0; i < count; i++)
                Add(new Position());
            SetAllFaceDown();
            OnCreatePool();
        }

        void OnCreatePool()
        {
            Logger.Log<Pool>("Runtime Pool Dispatched");
            Dispatcher.Notify<ICreatePool>(i => i.OnCreatePool(this));
        }

        void SetAllFaceDown()
        {
            foreach (var i in Units)
                i?.Value?.SetFaceUp(false);
        }

        void SetAllFaceUp()
        {
            foreach (var i in Units)
                i?.Value?.SetFaceUp(true);
        }
    }
}