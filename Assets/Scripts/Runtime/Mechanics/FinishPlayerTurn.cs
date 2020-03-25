namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of a player when it finishes the turn to the Listeners. </summary>
    [Event]
    public interface IFinishPlayerTurn
    {
        void OnFinishPlayerTurn(IPlayer player);
    }

    /// <summary> Finish Current player Turn Implementation. </summary>
    public class FinishPlayerTurn : BaseGameMechanics
    {
        public FinishPlayerTurn(IGame game) : base(game)
        {
        }


        /// <summary> Finish player turn logic. </summary>
        public void Execute()
        {
            if (!Game.IsTurnInProgress) return;
            if (!Game.IsGameStarted) return;
            if (Game.IsGameFinished) return;

            Game.IsTurnInProgress = false;
            OnFinishedCurrentPlayerTurn(Game.TurnLogic.CurrentPlayer);
        }

        /// <summary> Dispatch to the listeners. </summary>
        private void OnFinishedCurrentPlayerTurn(IPlayer currentPlayer)
        {
            Dispatcher.Notify<IFinishPlayerTurn>(i =>
                i.OnFinishPlayerTurn(currentPlayer));
        }
    }
}