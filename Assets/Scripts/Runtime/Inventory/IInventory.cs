namespace HexCardGame.Runtime
{
    public interface IInventory
    {
        SeatType Id { get; }
        IItem GetItem(string id);
        bool HasItem(IItem id, int amount);
        int GetAmount(IItem item);
        void AddItem(IItem item, int amount);
        void RemoveItem(IItem item, int amount);
    }
}