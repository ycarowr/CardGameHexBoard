namespace HexCardGame.Runtime.GamePool
{
    public interface IDataStorage<T>
    {
        bool HasData { get; }
        T Data { get; }
        void SetData(T value);
    }
}