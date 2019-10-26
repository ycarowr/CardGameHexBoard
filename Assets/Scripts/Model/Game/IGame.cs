using HexCardGame.Model.TurnLogic;

namespace HexCardGame.Model.Game
{
    /// <summary> A game interface. </summary>
    public interface IGame
    {
        EventsDispatcher Dispatcher { get; }

        BattleFsm BattleFsm { get; }

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