namespace HexCardGame.Runtime.GamePool
{
    public class Position: IValued<CardPool>
    {
        public bool HasValue => Value != null;
        public CardPool Value { get; private set; }

        public void SetValue(CardPool value)
        {
            if (!HasValue)
                Value = value;
        }
    }
}