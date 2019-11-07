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
        IDispatcher _dispatcher;
        GameData _gameData;

        /// <summary>  Handler for the state machine. Used to dispatch corotines. </summary>
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
            if (_gameData == null)
                _gameData = GameData.Load();
            if (_dispatcher == null)
                _dispatcher = EventsDispatcher.Load();
        }

        void Start() => StartBattle();

        /// <summary>  Start the battle. Called only once after being initialized by the Bootstrapper. </summary>
        [Button]
        void StartBattle()
        {
            _gameData.Initialize(this);
            _gameData.CurrentGameInstance.BattleFsm.StartBattle();
        }
    }
}