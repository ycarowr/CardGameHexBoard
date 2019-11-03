using Tools;
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
        Position[] Positions { get; }
        int Size();
        void AddCardAt(CardPool card, PoolPositionIndex index);
        void RemoveCardAt(PoolPositionIndex index);
        CardPool GetCardAt(PoolPositionIndex index);
        CardPool GetAndRemoveCardAt(PoolPositionIndex index);
        void FlipCardAt(PoolPositionIndex index);
        void Empty();
    }

    public class Pool : Position, IPool
    {
        public Pool(GameParameters gameParams, IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            CreatePool();
        }

        IDispatcher Dispatcher { get; }
        public Position[] Positions { get; private set; }

        public void FlipCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            card?.SetFaceUp(!card.IsFaceUp);
        }

        public int Size()
        {
            var size = 0;
            foreach (var i in Positions)
                if (i.HasData)
                    size++;
            return size;
        }

        public CardPool GetCardAt(PoolPositionIndex index) => Position(index)?.Data;

        public CardPool GetAndRemoveCardAt(PoolPositionIndex index)
        {
            var card = GetCardAt(index);
            RemoveCardAt(index);
            return card;
        }

        public void AddCardAt(CardPool card, PoolPositionIndex index) => Position(index)?.SetData(card);
        public void RemoveCardAt(PoolPositionIndex index) => Position(index)?.SetData(null);

        public void Empty()
        {
            foreach (var i in Positions)
                i.SetData(null);
        }

        Position Position(PoolPositionIndex index) => Positions[(int) index];

        void CreatePool()
        {
            var size = PoolPositionUtility.GetAllIndicesInt().Length;
            Positions = new Position[size];
            for (var i = 0; i < size; i++)
                Positions[i] = new Position();
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
            foreach (var i in Positions)
                i?.Data?.SetFaceUp(false);
        }

        void SetAllFaceUp()
        {
            foreach (var i in Positions)
                i?.Data?.SetFaceUp(true);
        }
    }
}