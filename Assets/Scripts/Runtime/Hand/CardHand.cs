using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardHand : ICard
    {
        public CardHand(CardData data) => SetData(data);
        public CardData Data { get; private set; }
        public void SetData(CardData data) => Data = data;
    }
}