namespace HexCardGame
{
    public partial class Player
    {
        #region Operations

        FinishTurnMechanics FinishTurn { get; }
        StartTurnMechanics StartTurn { get; }
        void IPlayer.StartTurn() => StartTurn.StartTurn();
        void IPlayer.FinishTurn() => FinishTurn.FinishTurn();

        #endregion
    }
}