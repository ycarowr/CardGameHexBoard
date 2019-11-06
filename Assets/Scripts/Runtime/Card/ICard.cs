using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public interface ICard
    {
        ICardData Data { get; }
    }
}