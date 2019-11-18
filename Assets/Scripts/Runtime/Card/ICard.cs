using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public interface ICard
    {
        ICardData Data { get; }
        int Cost { get; }
        int Score { get; }
    }
}