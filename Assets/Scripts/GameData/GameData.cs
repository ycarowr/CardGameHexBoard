using System.Collections.Generic;
using HexCardGame.Model.Game;
using UnityEngine;

namespace HexCardGame
{
    /// <summary>  Game data. </summary>
    [CreateAssetMenu(menuName = "GameData")]
    public class GameData : ScriptableObject
    {
        public const string Path = "GameData";

        [SerializeField] EventsDispatcher gameEvents;
        [SerializeField] GameParameters gameParameters;

        public IGame CurrentGameInstance { get; private set; }
        public static GameData Load() => Resources.Load<GameData>(Path);

        /// <summary>  Clears the game data. </summary>
        public void Clear() => CurrentGameInstance = null;

        /// <summary>  Create a new game data overriding the previous one. Produces Garbage. </summary>
        public void CreateGame(IGameController controller)
        {
            //create and connect players to their seats
            var user = new Player(gameParameters.Profiles.UserPlayer.id, gameParameters, gameEvents);

            //if the second player doesn't have a deck, send null
            var ai = new Player(gameParameters.Profiles.AiPlayer.id, gameParameters, gameEvents);

            //create game data
            CurrentGameInstance = new RuntimeGame(controller, new List<IPlayer> {user, ai}, gameParameters, gameEvents);
        }

        public IGame LoadGame() => null;

        /// <summary>  Initialize game data. </summary>
        public void Initialize(IGameController controller)
        {
            CurrentGameInstance = LoadGame();
            if (CurrentGameInstance == null)
                CreateGame(controller);
        }
    }
}