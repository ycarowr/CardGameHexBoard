namespace HexCardGame.Runtime
{
    public interface IInventory
    {
        PlayerId Id { get; }
        IItem GetItem(string id);
        bool HasItem(string id, int amount);
        int GetAmount(string id);
        void AddItem(IItem item, int amount);
        void RemoveItem(string id, int amount);
    }
}