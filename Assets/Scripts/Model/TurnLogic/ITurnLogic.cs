using System.Collections.Generic;

namespace HexCardGame.Model.TurnLogic
{
    public interface ITurnLogic
    {
        PlayerId CurrentPlayerId { get; }

        /// <summary> List with all the players that are playing the match. </summary>
        IPlayer[] Players { get; }

        /// <summary> Quantity of players playing this match. </summary>
        int PlayersCount { get; }

        /// <summary> Duration of the match in turns. </summary>
        int TurnCount { get; }

        /// <summary> Current player playing this turn. </summary>
        IPlayer CurrentPlayer { get; }

        /// <summary> GameController that started the match. </summary>
        IPlayer StarterPlayer { get; }

        /// <summary> Next player to play. </summary>
        IPlayer NextPlayer { get; }

        /// <summary> Sets the current player id. </summary>
        void UpdateCurrentPlayer();

        /// <summary> Calculus to decide which player starts the match. </summary>
        void DecideStarterPlayer();

        /// <summary> Returns whether is the player turn or not. </summary>
        bool IsMyTurn(IPlayer player);

        /// <summary> Returns whether is the player turn or not. </summary>
        bool IsMyTurn(PlayerId id);

        bool IsUser();
        bool IsEnemy();

        /// <summary> Returns a player opponent. </summary>
        IPlayer GetOpponent(IPlayer player);

        /// <summary> Returns a player based on its seat. </summary>
        IPlayer GetPlayer(PlayerId id);
    }
}