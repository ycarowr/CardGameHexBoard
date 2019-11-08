using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class BoardElement : ICard
    {
        public BoardElement(ICardData data, PlayerId ownerId)
        {
            OwnerId = ownerId;
            SetData(data);
        }

        public PlayerId OwnerId { get; }

        public ICardData Data { get; private set; }
        public void SetData(ICardData data) => Data = data;
    }
}