using System;
using System.Collections.Generic;
using HexCardGame.Model.Game;
using Tools.Patterns.Singleton;
using HexCardGame;
using UnityEngine;

namespace HexCardGame
{
    /// <summary>  Game data. </summary>
    [CreateAssetMenu (menuName = "GameData")]
    public class GameData : ScriptableObject
    {
        public const string Path = "GameData";
        public static GameData Load() => Resources.Load<GameData>(Path);
        
        [SerializeField] EventsDispatcher gameEvents;
        [SerializeField] GameParameters gameParameters;
        
        public IGame CurrentGameInstance { get; private set; }

        /// <summary>  Clears the game data. </summary>
        public void Clear() => CurrentGameInstance = null;

        /// <summary>  Create a new game data overriding the previous one. Produces Garbage. </summary>
        public void CreateGame(IGameController controller)
        {
            //create and connect players to their seats
            var playerBottom = new Player(gameParameters.Profiles.BottomPlayer.id, gameParameters, gameEvents);

            //if the second player doesn't have a deck, send null
            var playerTop = new Player(gameParameters.Profiles.TopPlayer.id, gameParameters, gameEvents);

            //create game data
            CurrentGameInstance = new RuntimeGame(controller, new List<IPlayer> {playerBottom, playerTop}, gameParameters, gameEvents);
        }

        public IGame LoadGame() => null;

        /// <summary>  Initialize game data. </summary>
        public void Initialize(IGameController controller)
        {
            CurrentGameInstance = LoadGame();
            if(CurrentGameInstance == null)
                CreateGame(controller);
            
            CurrentGameInstance.Fsm.StartBattle();
        }
    }
}