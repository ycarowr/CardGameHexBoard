namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of the winner after a game is finished to the Listeners. </summary>
    [Event]
    public interface IFinishGame
    {
        void OnFinishGame(IPlayer winner);
    }

    /// <summary> Finish Game Step Implementation. </summary>
    public class FinishGame : BaseGameMechanics
    {
        public FinishGame(IGame game) : base(game)
        {
        }

        public void Execute(IPlayer winner)
        {
            if (!Game.IsGameStarted)
                return;
            if (Game.IsGameFinished)
                return;

            Game.IsGameFinished = true;

            OnGameFinished(winner);
        }

        public void CheckWinCondition()
        {
        }

        /// <summary> Dispatch end game to the listeners. </summary>
        private void OnGameFinished(IPlayer winner)
        {
            Dispatcher.Notify<IFinishGame>(i => i.OnFinishGame(winner));
        }
    }
}