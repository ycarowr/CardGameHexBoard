namespace HexCardGame.Runtime.GamePool
{
    public class Position<T> : IDataStorage<T> where T : class
    {
        public bool IsLocked { get; private set; }
        public bool HasData => Data != null;
        public T Data { get; private set; }
        public void SetData(T value) => Data = value;
        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
    }
}