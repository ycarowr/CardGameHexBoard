namespace HexCardGame
{
    public partial class Player
    {
        StartTurnMechanics StartTurn { get; }
        void IPlayer.StartTurn() => StartTurn.StartTurn();
    }
}