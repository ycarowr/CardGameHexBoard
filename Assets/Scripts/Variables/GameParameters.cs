using System;
using HexCardGame.SharedData;
using UnityEngine;

namespace HexCardGame
{
    [CreateAssetMenu(menuName = "Game Parameters")]
    public class GameParameters : ScriptableObject
    {
        public const string Path = "References/GameParameters";

        //----------------------------------------------------------------------------------------------------------

        #region Board Configurations 

        public BoardData BoardData;

        #endregion

        public static GameParameters Load() => Resources.Load<GameParameters>(Path);

        #region Game Start

        public GameEventTimers Timers = new GameEventTimers();

        [Serializable]
        public class GameEventTimers
        {
            [Range(0.01f, 4)] [Tooltip("Time until AI does the turn.")]
            public float TimeUntilAiDoTurn = 2.5f;

            [Range(0.01f, 10)] [Tooltip("Time maximum for AI turns.")]
            public float TimeUntilAiFinishTurn = 3.5f;

            [Range(6f, 12f)] [Tooltip("Total user turn time")]
            public float TimeUntilFinishTurn = 6;

            [Tooltip("Time between Start Game event and First Player turn animation")] [Range(3f, 6f)]
            public float TimeUntilFirstPlayer = 3f;

            [Range(0.01f, 0.5f)] [Tooltip("Time between Load/Create and Pregame Event")]
            public float TimeUntilPreGameEvent = 0.01f;

            [Tooltip("Time between Pregame event and Start Game Event")] [Range(0.01f, 0.5f)]
            public float TimeUntilStartGameEvent = 0.01f;

            [Range(0.01f, 2f)] [Tooltip("Time until player starts the turn after the animation.")]
            public float TimeUntilStartTurn = 0.01f;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Players Configurations

        public PlayerProfiles Profiles = new PlayerProfiles();

        [Serializable]
        public class PlayerProfiles
        {
            [Tooltip("Configurations for Top player")]
            public Player AiPlayer = new Player
            {
                IsAi = true,
                id = PlayerId.Enemy
            };

            [Tooltip("Seat where the user player will be sitting.")]
            public PlayerId userId = PlayerId.User;

            [Tooltip("Configurations for Bottom player")]
            public Player UserPlayer = new Player
            {
                IsAi = false,
                id = PlayerId.User
            };

            [Serializable]
            public class Player
            {
                public PlayerId id;

                [Tooltip("Whether this player is driven by an AI system or not")]
                public bool IsAi;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}