using Tools;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GamePool
{
    [Event]
    public interface ICreatePool<T> where T : Coverable
    {
        void OnCreatePool(IPool<T> pool);
    }

    public interface IPool<T> where T : Coverable
    {
        Position<T>[] Positions { get; }
        int Size();
        void AddCardAt(T card, PoolPositionIndex index);
        void RemoveCardAt(PoolPositionIndex index);
        T GetCardAt(PoolPositionIndex index);
        T GetAndRemoveCardAt(PoolPositionIndex index);
        void UncoverAt(PoolPositionIndex index);
        void CoverAt(PoolPositionIndex index);
        void Empty();
    }

    public class Pool<T> : Position<T>, IPool<T> where T : Coverable
    {
        public Pool(GameParameters gameParams, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            CreatePool();
        }

        IDispatcher Dispatcher { get; }
        public Position<T>[] Positions { get; private set; }

        public void UncoverAt(PoolPositionIndex index) => GetCardAt(index).Uncover();

        public void CoverAt(PoolPositionIndex index) => GetCardAt(index).Cover();

        public int Size()
        {
            var size = 0;
            foreach (var i in Positions)
                if (i.HasData)
                    size++;
            return size;
        }

        public T GetCardAt(PoolPositionIndex index) => Position(index)?.Data;

        public T GetAndRemoveCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            RemoveCardAt(index);
            return card;
        }

        public void AddCardAt(T card, PoolPositionIndex index) => Position(index)?.SetData(card);
        public void RemoveCardAt(PoolPositionIndex index) => Position(index)?.SetData(null);

        public void Empty()
        {
            foreach (var i in Positions)
                i.SetData(null);
        }

        Position<T> Position(PoolPositionIndex index) => Positions[(int) index];

        void CreatePool()
        {
            var size = PoolPositionUtility.GetAllIndicesInt().Length;
            Positions = new Position<T>[size];
            for (var i = 0; i < size; i++)
                Positions[i] = new Position<T>();
            SetAllFaceDown();
            OnCreatePool();
        }

        void OnCreatePool()
        {
            Logger.Log<Pool<T>>("Runtime Pool Dispatched");
            Dispatcher.Notify<ICreatePool<T>>(i => i.OnCreatePool(this));
        }

        void SetAllFaceDown()
        {
            foreach (var i in Positions)
                i?.Data?.Cover();
        }

        void SetAllFaceUp()
        {
            foreach (var i in Positions)
                i?.Data?.Uncover();
        }
    }
}