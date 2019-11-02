using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardBoard : ICard
    {
        public CardBoard(CardData data) => SetData(data);
        public CardData Data { get; private set; }
        public void SetData(CardData data) => Data = data;
    }
}