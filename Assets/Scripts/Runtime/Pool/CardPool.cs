using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardPool : ICard
    {
        public CardPool(CardData data) => SetData(data);
        public CardData Data { get; private set; }
        public void SetData(CardData data) => Data = data;
    }
}