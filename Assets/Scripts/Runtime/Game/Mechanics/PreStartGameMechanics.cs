namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of the players right before the game start. </summary>
    [Event]
    public interface IPreGameStart
    {
        void OnPreGameStart(IPlayer[] players);
    }

    /// <summary> Pre Start Game Step Implementation. </summary>
    public class PreStartGameMechanics : BaseGameMechanics
    {
        public PreStartGameMechanics(IGame game) : base(game)
        {
        }

        /// <summary> Execution of start game</summary>
        public void Execute()
        {
            if (Game.IsGameStarted)
                return;

            OnGamePreStarted(Game.TurnLogic.Players);
        }

        /// <summary> Dispatch pre start game event to the listeners</summary>
        void OnGamePreStarted(IPlayer[] players) =>
            Dispatcher.Notify<IPreGameStart>(i => i.OnPreGameStart(players));
    }
}