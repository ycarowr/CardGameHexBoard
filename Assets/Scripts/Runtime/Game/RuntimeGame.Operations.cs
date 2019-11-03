namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Operations

        public void PreStartGame() => _preStartGame.Execute();
        public void StartGame() => _startGame.Execute();
        public void StartPlayerTurn() => _startPlayerTurn.Execute();
        public void FinishPlayerTurn() => _finishPlayerTurn.Execute();
        public void DrawCardFromLibrary(IPlayer player) => _handLibrary.DrawCard(player);

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            _finishGame.Execute(player);
        }

        #endregion
    }
}