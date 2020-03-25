using Tools.Patterns.Observer;
using Tools.Patterns.StateMachine;
using UnityEngine;

namespace HexCardGame
{
    /// <summary> Broadcast of restart game. </summary>
    [Event]
    public interface IRestartGame
    {
        void OnRestart();
    }

    public interface IGameController : IStateMachineHandler
    {
        MonoBehaviour MonoBehaviour { get; }
        void RestartGameImmediately();
    }

    public class GameController : MonoBehaviour, IGameController
    {
        private IDispatcher _dispatcher;
        private GameData _gameData;
        private GameParameters _gameParameters;

        /// <summary>  Handler for the state machine. Used to dispatch corotines. </summary>
        public MonoBehaviour MonoBehaviour => this;

        [Button]
        public void RestartGameImmediately()
        {
            _dispatcher.Notify<IRestartGame>(i => i.OnRestart());
            _gameData.Clear();
        }

        private void Awake()
        {
            if (_gameData == null)
                _gameData = GameData.Load();
            if (_dispatcher == null)
                _dispatcher = EventsDispatcher.Load();
            if (_gameParameters == null)
                _gameParameters = GameParameters.Load();
        }

        [Button]
        private void StartLocalGame()
        {
            var localPlayerSeat = _gameParameters.Profiles.localPlayer.seat;
            var remotePlayerSeat = _gameParameters.Profiles.remotePlayer.seat;

            var localPlayerNetworkId = 0;
            var remotePlayerNetworkId = 1;

            var localPlayer = new Player(localPlayerNetworkId, localPlayerSeat, _gameParameters, _dispatcher);
            var remotePlayer = new Player(remotePlayerNetworkId, remotePlayerSeat, _gameParameters, _dispatcher);

            StartBattle(localPlayer, remotePlayer);
        }


        /// <summary>  Start the battle. Called only once after being initialized by the Bootstrapper. </summary>
        public void StartBattle(IPlayer localPlayer, IPlayer remotePlayer)
        {
            _gameData.CreateGame(this, localPlayer, remotePlayer);
            _gameData.CurrentGameInstance.BattleFsm.StartBattle();
        }
    }
}