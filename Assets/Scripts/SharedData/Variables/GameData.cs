using HexCardGame.Runtime.Game;
using Tools.Patterns.Observer;
using UnityEngine;

namespace HexCardGame
{
    /// <summary>  Game data. </summary>
    [CreateAssetMenu(menuName = "Variables/GameData")]
    public class GameData : ScriptableObject
    {
        public const string Path = "Variables/GameData";
        IDispatcher _dispatcher;
        [SerializeField] GameParameters gameParameters;

        public IGame CurrentGameInstance { get; private set; }
        public static GameData Load() => Resources.Load<GameData>(Path);

        /// <summary>  Clears the game data. </summary>
        public void Clear() => CurrentGameInstance = null;

        void CreateGame(IGameController controller)
        {
            var turnBasedArgs = new RuntimeGame.GameArgs
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
            if (_dispatcher == null)
                _dispatcher = EventsDispatcher.Load();
        }
    }
}