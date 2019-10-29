using System;
using System.Collections.Generic;
using HexCardGame.Model.Game;
using Tools.Patterns.Observer;
using UnityEngine;

namespace HexCardGame
{
    /// <summary>  Game data. </summary>
    [CreateAssetMenu(menuName = "References/GameData")]
    public class GameDataReference : ScriptableObject
    {
        public const string Path = "References/GameData";
        IDispatcher _dispatcher;
        [SerializeField] GameParametersReference gameParameters;

        public IGame CurrentGameInstance { get; private set; }
        public static GameDataReference Load() => Resources.Load<GameDataReference>(Path);

        /// <summary>  Clears the game data. </summary>
        public void Clear() => CurrentGameInstance = null;

        void CreateGame(IGameController controller)
        {
            var turnBasedArgs = new RuntimeGame.RuntimeGameArgs
            {
                Controller = controller,
                GameParameters = gameParameters,
                Dispatcher = _dispatcher
            };

            //create game data
            CurrentGameInstance = new RuntimeGame(turnBasedArgs);
        }

        IGame LoadGame() => null;

        /// <summary>  Initialize game data. </summary>
        public void Initialize(IGameController controller)
        {
            CurrentGameInstance = LoadGame();
            if (CurrentGameInstance == null)
                CreateGame(controller);
        }

        void OnEnable()
        {
            if(_dispatcher == null)
                _dispatcher = EventsDispatcherReference.Load();
        }
    }
}