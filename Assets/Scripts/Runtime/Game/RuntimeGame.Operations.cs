namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Operations

        public void PreStartGame() => PreStartGameMechanics.Execute();
        public void StartGame() => StartGameMechanics.Execute();
        public void StartCurrentPlayerTurn() => StartPlayerTurnMechanics.Execute();
        public void FinishCurrentPlayerTurn() => FinishPlayerTurnMechanics.Execute();

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            FinishGameMechanics.Execute(player);
        }

        #endregion
    }
}