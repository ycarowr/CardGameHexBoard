using Tools;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GamePool
{
    [Event]
    public interface ICreatePool<T> where T : class
    {
        void OnCreatePool(IPool<T> pool);
    }

    public interface IPool<T> : IPoolStorage<T> where T : class
    {
        int Size();
        void Lock(PositionId positionId);
        void Unlock(PositionId positionId);
        bool IsPositionLocked(PositionId positionId);
    }

    public interface IPoolStorage<T> where T : class
    {
        void Clear();
        void AddCardAt(T card, PositionId id);
        void RemoveCardAt(PositionId id);
        T GetCardAt(PositionId id);
        T GetAndRemoveCardAt(PositionId id);
        bool HasDataAt(PositionId id);
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

        public bool IsPositionLocked(PositionId positionId) => Get(positionId).IsLocked;

        public void Unlock(PositionId positionId)
        {
            switch (positionId)
            {
                case PositionId.Zero:
                    break;
                case PositionId.One:
                    Get(PositionId.Zero).Unlock();
                    break;
                case PositionId.Two:
                    Get(PositionId.Zero).Unlock();
                    break;
                case PositionId.Three:
                    Get(PositionId.One).Unlock();
                    break;
                case PositionId.Four:
                    Get(PositionId.One).Unlock();
                    Get(PositionId.Two).Unlock();
                    break;
                case PositionId.Five:
                    Get(PositionId.Two).Unlock();
                    break;
                case PositionId.Six:
                    Get(PositionId.Three).Unlock();
                    break;
                case PositionId.Seven:
                    Get(PositionId.Three).Unlock();
                    Get(PositionId.Four).Unlock();
                    break;
                case PositionId.Eight:
                    Get(PositionId.Five).Unlock();
                    Get(PositionId.Four).Unlock();
                    break;
                case PositionId.Nine:
                    Get(PositionId.Five).Unlock();
                    break;
            }
        }

        public void Lock(PositionId positionId)
        {
            switch (positionId)
            {
                case PositionId.Zero:
                    break;
                case PositionId.One:
                    Get(PositionId.Zero).Lock();
                    break;
                case PositionId.Two:
                    Get(PositionId.Zero).Lock();
                    break;
                case PositionId.Three:
                    Get(PositionId.One).Lock();
                    break;
                case PositionId.Four:
                    Get(PositionId.One).Lock();
                    Get(PositionId.Two).Lock();
                    break;
                case PositionId.Five:
                    Get(PositionId.Two).Lock();
                    break;
                case PositionId.Six:
                    Get(PositionId.Three).Lock();
                    break;
                case PositionId.Seven:
                    Get(PositionId.Three).Lock();
                    Get(PositionId.Four).Lock();
                    break;
                case PositionId.Eight:
                    Get(PositionId.Five).Lock();
                    Get(PositionId.Four).Lock();
                    break;
                case PositionId.Nine:
                    Get(PositionId.Five).Lock();
                    break;
            }
        }

        Position<T> Get(PositionId positionId) => Positions[(int) positionId];

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

        #region Storage

        public T GetCardAt(PositionId id) => Get(id)?.Data;

        public T GetAndRemoveCardAt(PositionId id)
        {
            var card = GetCardAt(id);
            RemoveCardAt(id);
            return card;
        }

        public void AddCardAt(T card, PositionId id) => Get(id)?.SetData(card);
        public void RemoveCardAt(PositionId id) => Get(id)?.SetData(null);
        public bool HasDataAt(PositionId id) => Get(id).HasData;

        public void Clear()
        {
            foreach (var i in Positions)
                i.SetData(null);
        }

        #endregion
    }
}