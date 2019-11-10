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

    public partial class Pool<T> : IPool<T> where T : class
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

        Position<T> Get(PositionId positionId) => Positions[(int) positionId];

        void CreatePool()
        {
            var positions = PoolPositionUtility.GetAllIndices();
            var size = positions.Length;
            Positions = new Position<T>[size];
            foreach (var i in positions)
                Positions[(int) i] = new Position<T>(i, Dispatcher);

            OnCreatePool();
        }

        void OnCreatePool() => Dispatcher.Notify<ICreatePool<T>>(i => i.OnCreatePool(this));

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