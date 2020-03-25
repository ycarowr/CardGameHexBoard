using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardHand : ICard
    {
        public CardHand(ICardData data)
        {
            SetData(data);
        }

        public ICardData Data { get; private set; }

        public int Cost { get; private set; }
        public int Score { get; private set; }

        public void SetData(ICardData data)
        {
            Data = data;
            UpdateData();
        }

        private void UpdateData()
        {
            Cost = Data.Cost;
            Score = Data.Score;
        }
    }
}