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
                Dispatcher = _dispatcher,
                GameParameters = gameParameters
            };
            CurrentGameInstance = new RuntimeGame(turnBasedArgs);
        }

        /// <summary>  Initialize game data. </summary>
        public void Initialize(IGameController controller) => CreateGame(controller);

        void OnEnable()
        {
            if (_dispatcher == null)
                _dispatcher = EventsDispatcher.Load();
        }
    }
}