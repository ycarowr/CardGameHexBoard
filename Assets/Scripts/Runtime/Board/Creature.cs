using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class Creature : ICard
    {
        public Creature(CardData data) => SetData(data);
        public CardData Data { get; private set; }
        public void SetData(CardData data) => Data = data;
    }
}