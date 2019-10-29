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

    /// <summary>
    ///     Main Controller. Holds the FSM which controls the game flow. Also provides access to the players
    /// </summary>
    public class GameController : MonoBehaviour, IGameController
    {
        IDispatcher _dispatcher;
        GameDataReference _gameDataReference;

        /// <summary>  Handler for the state machine. Used to dispatch coroutines. </summary>
        public MonoBehaviour MonoBehaviour => this;

        [Button]
        public void RestartGameImmediately()
        {
            _dispatcher.Notify<IRestartGame>(i => i.OnRestart());
            _gameDataReference.Clear();
            StartBattle();
        }

        void Awake()
        {
            if(!_gameDataReference)
                _gameDataReference = GameDataReference.Load();
            if(_dispatcher == null)
                _dispatcher = EventsDispatcherReference.Load();
        }

        void Start() => StartBattle();

        /// <summary>  Start the battle. Called only once after being initialized by the Bootstrapper. </summary>
        [Button] void StartBattle()
        {
            _gameDataReference.Initialize(this);
            _gameDataReference.CurrentGameInstance.BattleFsm.StartBattle();
        }
    }
}