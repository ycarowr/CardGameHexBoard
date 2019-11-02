namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of the starter player to the Listeners. </summary>
    [Event]
    public interface IStartGame
    {
        void OnStartGame(IPlayer starter);
    }

    /// <summary> Start Game Step Implementation. </summary>
    public class StartGameMechanics : BaseGameMechanics
    {
        public StartGameMechanics(IGame game) : base(game)
        {
        }

        /// <summary> Execution of start game</summary>
        public void Execute()
        {
            if (Game.IsGameStarted) return;

            Game.IsGameStarted = true;

            //calculus of the starting player
            Game.TurnLogic.DecideStarterPlayer();

            OnGameStarted(Game.TurnLogic.StarterPlayer);
        }

        /// <summary> Dispatch start game event to the listeners. </summary>
        void OnGameStarted(IPlayer starterPlayer) => Dispatcher.Notify<IStartGame>(i => i.OnStartGame(starterPlayer));
    }
}