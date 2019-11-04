namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of the starter player to the Listeners. </summary>
    [Event]
    public interface IStartGame
    {
        void OnStartGame(IPlayer starter);
    }

    /// <summary> Start Game Step Implementation. </summary>
    public class StartGame : BaseGameMechanics
    {
        public StartGame(IGame game) : base(game)
        {
        }

        /// <summary> Execution of start game</summary>
        public void Execute()
        {
            if (Game.IsGameStarted) return;

            Game.IsGameStarted = true;

            Game.TurnLogic.DecideStarterPlayer();

            DrawStartingHands();

            OnGameStarted(Game.TurnLogic.StarterPlayer);
        }

        void DrawStartingHands()
        {
            foreach (var player in Game.TurnLogic.Players)
                for (var i = 0; i < Parameters.Hand.StartingHandCount; i++)
                    Game.DrawCardFromLibrary(player);
        }

        /// <summary> Dispatch start game event to the listeners. </summary>
        void OnGameStarted(IPlayer starterPlayer) => Dispatcher.Notify<IStartGame>(i => i.OnStartGame(starterPlayer));
    }
}