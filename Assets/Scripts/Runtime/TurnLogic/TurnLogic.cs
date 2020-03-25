using System;
using Random = UnityEngine.Random;

namespace HexCardGame.Runtime.GameTurn
{
    /// <summary> This class decides which player goes first and which player goes next. </summary>
    public class TurnLogic : ITurnLogic
    {
        public TurnLogic(IPlayer[] players)
        {
            if (players == null)
                throw new ArgumentException("A Null List is not a valid argument to Create a TurnLogic");
            if (players.Length < 1)
                throw new ArgumentException("Invalid number of players: " + PlayersCount);

            Players = players;
            TurnCount = 0;
        }

        #region Properties

        public SeatType CurrentSeatType { get; private set; }
        public SeatType NextSeatType => CurrentPlayer.Id == SeatType.Bottom ? SeatType.Top : SeatType.Bottom;
        public SeatType StarterSeatType { get; private set; }
        public IPlayer[] Players { get; }
        public int TurnCount { get; private set; }
        public IPlayer CurrentPlayer => GetPlayer(CurrentSeatType);
        public IPlayer NextPlayer => GetPlayer(NextSeatType);
        public IPlayer StarterPlayer => GetPlayer(StarterSeatType);
        public int PlayersCount => Players.Length;
        public bool IsMyTurn(IPlayer player) => CurrentPlayer == player;
        public bool IsMyTurn(SeatType id) => CurrentSeatType == id;

        #endregion

        #region Operations

        /// <summary> Assign next player to the current player. </summary>
        public void UpdateCurrentPlayer()
        {
            //increment turn count
            TurnCount++;

            //not on the first turn of the match
            if (TurnCount == 1)
                return;

            //update current player
            CurrentSeatType = NextSeatType;
        }

        /// <summary> Decides which player goes first Randomly. </summary>
        public void DecideStarterPlayer()
        {
            var randomIndex = Random.Range(0, PlayersCount);
            randomIndex = 0;
            StarterSeatType = Players[randomIndex].Id;
            CurrentSeatType = StarterSeatType;
        }

        public IPlayer GetOpponent(IPlayer player) => player.Id == SeatType.Bottom
            ? GetPlayer(SeatType.Top)
            : GetPlayer(SeatType.Bottom);

        public IPlayer GetPlayer(SeatType id)
        {
            foreach (var player in Players)
                if (player.Id == id)
                    return player;

            return null;
        }

        public bool IsUser() => CurrentSeatType == SeatType.Bottom;
        public bool IsEnemy() => CurrentSeatType == SeatType.Top;

        public void SetCurrentSeat(SeatType current) => CurrentSeatType = current;

        public void SetStarterSeat(SeatType first) => StarterSeatType = first;

        #endregion
    }
}