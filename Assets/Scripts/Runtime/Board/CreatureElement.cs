using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public abstract class BoardElement
    {
    }

    public class CreatureElement : ICard
    {
        public CreatureElement(ICardData data, PlayerId ownerId)
        {
            OwnerId = ownerId;
            SetData(data);
        }


        public PlayerId OwnerId { get; }

        public ICardData Data { get; private set; }

        public int Cost { get; private set; }
        public int Score { get; private set; }

        public void SetData(ICardData data)
        {
            Data = data;
            UpdateData();
        }

        void UpdateData()
        {
            Cost = Data.Cost;
            Score = Data.Score;
        }
    }
}