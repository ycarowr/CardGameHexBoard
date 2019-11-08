namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of a player when it starts the turn to the Listeners. </summary>
    [Event]
    public interface IStartPlayerTurn
    {
        void OnStartPlayerTurn(IPlayer player);
    }

    /// <summary> Start Current player Turn Implementation. </summary>
    public class StartPlayerTurn : BaseGameMechanics
    {
        public StartPlayerTurn(IGame game) : base(game)
        {
        }

        /// <summary> Start current player turn logic. </summary>
        public void Execute()
        {
            if (Game.IsTurnInProgress)
                return;
            if (!Game.IsGameStarted)
                return;
            if (Game.IsGameFinished)
                return;


            Game.IsTurnInProgress = true;
            Game.TurnLogic.UpdateCurrentPlayer();
            var playerId = Game.TurnLogic.CurrentPlayerId;
            Game.DrawCardFromLibrary(playerId);
            OnStartedCurrentPlayerTurn(Game.TurnLogic.CurrentPlayer);
        }

        /// <summary> Dispatch start current player turn to the listeners. </summary>
        void OnStartedCurrentPlayerTurn(IPlayer currentPlayer) =>
            Dispatcher.Notify<IStartPlayerTurn>(i =>
                i.OnStartPlayerTurn(currentPlayer));
    }
}