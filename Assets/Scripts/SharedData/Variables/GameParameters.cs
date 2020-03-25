using System;
using HexCardGame.SharedData;
using UnityEngine;

namespace HexCardGame
{
    [CreateAssetMenu(menuName = "Game Parameters")]
    public class GameParameters : ScriptableObject
    {
        public const string Path = "Variables/GameParameters";

        //----------------------------------------------------------------------------------------------------------

        public BoardData BoardData;

        public static GameParameters Load()
        {
            return Resources.Load<GameParameters>(Path);
        }

        //----------------------------------------------------------------------------------------------------------

        #region Amounts

        [Serializable]
        public class AmountsConfigs
        {
            [Range(0, 5)] public int ActionPointsConsume;
            [Range(0, 5)] public int ActionPointsPerTurn;
            [Range(0, 5)] public int GoldPerTurn;
            [Range(1, 10)] public int MaxHandSize;
            [Range(0, 5)] public int StartingGold;
            [Range(1, 10)] public int StartingHandCount;
        }

        public AmountsConfigs Amounts = new AmountsConfigs();

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Game Start

        public GameEventTimers Timers = new GameEventTimers();

        [Serializable]
        public class GameEventTimers
        {
            [Range(0.01f, 4), Tooltip("Time until AI does the turn.")]
            public float TimeUntilAiDoTurn = 2.5f;

            [Range(0.01f, 10), Tooltip("Time maximum for AI turns.")]
            public float TimeUntilAiFinishTurn = 3.5f;

            [Range(6f, 12f), Tooltip("Total user turn time")]
            public float TimeUntilFinishTurn = 6;

            [Tooltip("Time between Start Game event and First Player turn animation"), Range(3f, 6f)]
            public float TimeUntilFirstPlayer = 3f;

            [Range(0.01f, 0.5f), Tooltip("Time between Load/Create and Pregame Event")]
            public float TimeUntilPreGameEvent = 0.01f;

            [Tooltip("Time between Pregame event and Start Game Event"), Range(0.01f, 0.5f)]
            public float TimeUntilStartGameEvent = 0.01f;

            [Range(0.01f, 2f), Tooltip("Time until player starts the turn after the animation.")]
            public float TimeUntilStartTurn = 0.01f;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Players Configurations

        public PlayerProfiles Profiles = new PlayerProfiles();

        [Serializable]
        public class PlayerProfiles
        {
            [Tooltip("Configurations for Bottom player")]
            public Player localPlayer = new Player
            {
                isAi = false,
                seat = SeatType.Bottom
            };

            [Tooltip("Seat where the user player will be sitting.")]
            public SeatType localPlayerSeat = SeatType.Bottom;

            public Player remotePlayer = new Player
            {
                isAi = true,
                seat = SeatType.Top
            };

            [Serializable]
            public class Player
            {
                public bool isAi;
                public PlayerDeck libraryData;
                public SeatType seat;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}