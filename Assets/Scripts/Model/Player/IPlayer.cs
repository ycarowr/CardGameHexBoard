namespace HexCardGame
{
    public interface IPlayer
    {
        GameParameters GameParameters { get; }
        PlayerId Id { get; }
        bool IsUser { get; }
        void StartTurn();
        void FinishTurn();
    }
}