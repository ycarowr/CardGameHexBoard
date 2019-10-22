using HexCardGame.Model.Game;
using Tools.Patterns.GameEvents;
using Tools.Patterns.Singleton;
using Tools.Patterns.StateMachine;
using UnityEngine;

namespace HexCardGame
{
    public interface IGameController : IStateMachineHandler
    {
        MonoBehaviour MonoBehaviour { get; }
    }
    
    /// <summary>
    ///     Main Controller. Holds the FSM which controls the game flow. Also provides access to the players
    /// </summary>
    public class GameController : MonoBehaviour, IGameController
    {
        /// <summary>  Handler for the state machine. Used to dispatch coroutines. </summary>
        public MonoBehaviour MonoBehaviour => this;
        
//        [Button]
//        public void RestartGameImmediately()
//        {
//            gameEvents.Notify<IRestartGame>(i => i.OnRestart());
//            gameData.CreateGame();
//            StartBattle();
//        }

        void Start() => StartBattle();

        /// <summary>  Start the battle. Called only once after being initialized by the Bootstrapper. </summary>
        [Button]
        public void StartBattle()
        {
            var gameData = GameData.Load();
            gameData.Initialize(this);
        }
    }
}