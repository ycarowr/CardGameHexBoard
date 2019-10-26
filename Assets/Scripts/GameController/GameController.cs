using Tools.Patterns.StateMachine;
using UnityEngine;

namespace HexCardGame
{
    public interface IGameController : IStateMachineHandler
    {
        MonoBehaviour MonoBehaviour { get; }
        void RestartGameImmediately();
    }

    /// <summary>
    ///     Main Controller. Holds the FSM which controls the game flow. Also provides access to the players
    /// </summary>
    public class GameController : MonoBehaviour, IGameController
    {
        EventsDispatcher _dispatcher;
        GameData _gameData;

        /// <summary>  Handler for the state machine. Used to dispatch coroutines. </summary>
        public MonoBehaviour MonoBehaviour => this;

        [Button]
        public void RestartGameImmediately()
        {
            _dispatcher.Notify<IRestartGame>(i => i.OnRestart());
            _gameData.Clear();
            StartBattle();
        }

        void Awake()
        {
            _gameData = GameData.Load();
            _dispatcher = EventsDispatcher.Load();
        }

        void Start() => StartBattle();

        /// <summary>  Start the battle. Called only once after being initialized by the Bootstrapper. </summary>
        [Button]
        public void StartBattle() => _gameData.Initialize(this);
    }
}