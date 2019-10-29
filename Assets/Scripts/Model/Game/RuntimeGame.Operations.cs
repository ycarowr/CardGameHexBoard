namespace HexCardGame.Model.Game
{
    public partial class RuntimeGame
    {
        #region Operations

        public void PreStartGame() => ProcessPreStartGame.Execute();
        public void StartGame() => ProcessStartGame.Execute();
        public void StartCurrentPlayerTurn() => ProcessStartPlayerTurn.Execute();
        public void FinishCurrentPlayerTurn() => ProcessFinishPlayerTurn.Execute();

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            ProcessFinishGame.Execute(player);
        }

        #endregion
    }
}