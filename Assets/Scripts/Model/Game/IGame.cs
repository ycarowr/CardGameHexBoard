using System.Collections.Generic;
using HexCardGame.Model.TurnLogic;
using HexCardGame;

namespace HexCardGame.Model.Game
{
    /// <summary> A game interface. </summary>
    public interface IGame
    {
        GameParameters GameParameters { get; }
        EventsDispatcher GameEvents { get; }
        List<BaseGameMechanics> Mechanics { get; }

        TurnBasedFsm Fsm { get; }

        List<IPlayer> Players { get; }

        ITurnLogic TurnLogic { get; }

        bool IsGameStarted { get; set; }

        bool IsGameFinished { get; set; }

        bool IsTurnInProgress { get; set; }

        void PreStartGame();

        void StartGame();

        void StartCurrentPlayerTurn();

        void FinishCurrentPlayerTurn();

        void ExecuteAiTurn(PlayerId id);
        void ForceWin(PlayerId id);
    }
}