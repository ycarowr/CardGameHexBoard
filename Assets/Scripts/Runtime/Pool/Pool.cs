using Tools;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GamePool
{
    [Event]
    public interface ICreatePool<T> where T : class
    {
        void OnCreatePool(IPool<T> pool);
    }

    public interface IPool<T> where T : class
    {
        Position<T>[] Positions { get; }
        int Size();
        void AddCardAt(T card, PoolPositionIndex index);
        void RemoveCardAt(PoolPositionIndex index);
        T GetCardAt(PoolPositionIndex index);
        T GetAndRemoveCardAt(PoolPositionIndex index);
        bool HasDataAt(PoolPositionIndex index);
        void Lock(PoolPositionIndex index);
        void Unlock(PoolPositionIndex index);
        void Empty();
    }

    public class Pool<T> : Position<T>, IPool<T> where T : class
    {
        public Pool(GameParameters gameParams, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            CreatePool();
        }

        IDispatcher Dispatcher { get; }
        public Position<T>[] Positions { get; private set; }

        public int Size()
        {
            var size = 0;
            foreach (var i in Positions)
                if (i.HasData)
                    size++;
            return size;
        }

        public T GetCardAt(PoolPositionIndex index) => Get(index)?.Data;

        public T GetAndRemoveCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            RemoveCardAt(index);
            return card;
        }

        public void AddCardAt(T card, PoolPositionIndex index) => Get(index)?.SetData(card);
        public void RemoveCardAt(PoolPositionIndex index) => Get(index)?.SetData(null);
        public bool HasDataAt(PoolPositionIndex index) => Get(index).HasData;

        public void Empty()
        {
            foreach (var i in Positions)
                i.SetData(null);
        }

        public void Unlock(PoolPositionIndex positionIndex)
        {
            switch (positionIndex)
            {
                case PoolPositionIndex.Zero:
                    break;
                case PoolPositionIndex.One:
                    Get(PoolPositionIndex.Zero).Unlock();
                    break;
                case PoolPositionIndex.Two:
                    Get(PoolPositionIndex.Zero).Unlock();
                    break;
                case PoolPositionIndex.Three:
                    Get(PoolPositionIndex.One).Unlock();
                    break;
                case PoolPositionIndex.Four:
                    Get(PoolPositionIndex.One).Unlock();
                    Get(PoolPositionIndex.Two).Unlock();
                    break;
                case PoolPositionIndex.Five:
                    Get(PoolPositionIndex.Two).Unlock();
                    break;
                case PoolPositionIndex.Six:
                    Get(PoolPositionIndex.Three).Unlock();
                    break;
                case PoolPositionIndex.Seven:
                    Get(PoolPositionIndex.Three).Unlock();
                    Get(PoolPositionIndex.Four).Unlock();
                    break;
                case PoolPositionIndex.Eight:
                    Get(PoolPositionIndex.Five).Unlock();
                    Get(PoolPositionIndex.Four).Unlock();
                    break;
                case PoolPositionIndex.Nine:
                    Get(PoolPositionIndex.Five).Unlock();
                    break;
            }
        }

        public void Lock(PoolPositionIndex positionIndex)
        {
            switch (positionIndex)
            {
                case PoolPositionIndex.Zero:
                    break;
                case PoolPositionIndex.One:
                    Get(PoolPositionIndex.Zero).Lock();
                    break;
                case PoolPositionIndex.Two:
                    Get(PoolPositionIndex.Zero).Lock();
                    break;
                case PoolPositionIndex.Three:
                    Get(PoolPositionIndex.One).Lock();
                    break;
                case PoolPositionIndex.Four:
                    Get(PoolPositionIndex.One).Lock();
                    Get(PoolPositionIndex.Two).Lock();
                    break;
                case PoolPositionIndex.Five:
                    Get(PoolPositionIndex.Two).Lock();
                    break;
                case PoolPositionIndex.Six:
                    Get(PoolPositionIndex.Three).Lock();
                    break;
                case PoolPositionIndex.Seven:
                    Get(PoolPositionIndex.Three).Lock();
                    Get(PoolPositionIndex.Four).Lock();
                    break;
                case PoolPositionIndex.Eight:
                    Get(PoolPositionIndex.Five).Lock();
                    Get(PoolPositionIndex.Four).Lock();
                    break;
                case PoolPositionIndex.Nine:
                    Get(PoolPositionIndex.Five).Lock();
                    break;
            }
        }

        Position<T> Get(PoolPositionIndex index) => Positions[(int) index];

        void CreatePool()
        {
            var size = PoolPositionUtility.GetAllIndicesInt().Length;
            Positions = new Position<T>[size];
            for (var i = 0; i < size; i++)
                Positions[i] = new Position<T>();
            OnCreatePool();
        }

        void OnCreatePool()
        {
            Logger.Log<Pool<T>>("Runtime Pool Dispatched");
            Dispatcher.Notify<ICreatePool<T>>(i => i.OnCreatePool(this));
        }
    }
}