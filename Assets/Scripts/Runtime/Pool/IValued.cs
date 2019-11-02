namespace HexCardGame.Runtime.GamePool
{
    public interface IValued<T>
    {
        bool HasValue { get; }
        T Value { get; }
        void SetValue(T value);
    }
}