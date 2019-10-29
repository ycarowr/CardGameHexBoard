namespace HexCardGame
{
    public partial class Player
    {
        FinishTurnMechanics FinishTurn { get; }
        void IPlayer.FinishTurn() => FinishTurn.FinishTurn();
    }
}