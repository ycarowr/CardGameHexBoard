using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.GamePool
{
    [Event]
    public interface ILockPosition
    {
        void OnLockPosition(PositionId positionId);
    }

    [Event]
    public interface IUnlockPosition
    {
        void OnUnlockPosition(PositionId positionId);
    }

    public class Position<T> : IDataStorage<T> where T : class
    {
        public Position(PositionId positionId, IDispatcher dispatcher)
        {
            PositionId = positionId;
            Dispatcher = dispatcher;
        }

        public PositionId PositionId { get; }
        public IDispatcher Dispatcher { get; }

        public bool IsLocked { get; private set; }
        public bool HasData => Data != null;
        public T Data { get; private set; }
        public void SetData(T value) => Data = value;

        public void Lock()
        {
            IsLocked = true;
            Dispatcher.Notify<ILockPosition>(i => i.OnLockPosition(PositionId));
        }

        public void Unlock()
        {
            IsLocked = false;
            Dispatcher.Notify<IUnlockPosition>(i => i.OnUnlockPosition(PositionId));
        }
    }
}