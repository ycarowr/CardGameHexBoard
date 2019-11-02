namespace HexCardGame.Runtime.GamePool
{
    public class Position : IValued<CardPool>
    {
        public bool HasValue => Value != null;
        public CardPool Value { get; private set; }

        public void SetValue(CardPool value) => Value = value;
    }
}