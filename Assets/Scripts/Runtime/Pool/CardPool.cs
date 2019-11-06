using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardPool : ICard
    {
        public CardPool(ICardData data) => SetData(data);
        public ICardData Data { get; private set; }
        public void SetData(ICardData data) => Data = data;
    }
}