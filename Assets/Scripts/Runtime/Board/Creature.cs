using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class Creature : ICard
    {
        public Creature(ICardData data) => SetData(data);
        public ICardData Data { get; private set; }
        public void SetData(ICardData data) => Data = data;
    }
}