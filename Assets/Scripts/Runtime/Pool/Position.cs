namespace HexCardGame.Runtime.GamePool
{
    public class Position : IDataStorage<CardPool>
    {
        public bool HasData => Data != null;
        public CardPool Data { get; private set; }

        public void SetData(CardPool value) => Data = value;
    }
}