using HexCardGame.Runtime.TurnLogic;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    /// <summary> A turn-based game interface. </summary>
    public interface ITurnMechanics
    {
        ITurnLogic TurnLogic { get; }
        BattleFsm BattleFsm { get; }
        bool IsTurnInProgress { get; set; }
        void PreStartGame();
        void StartGame();
        void StartCurrentPlayerTurn();
        void FinishCurrentPlayerTurn();
    }

    /// <summary> A game interface. </summary>
    public interface IGame : ITurnMechanics
    {
        IDispatcher Dispatcher { get; }
        bool IsGameStarted { get; set; }
        bool IsGameFinished { get; set; }
        void ExecuteAiTurn(PlayerId id);
        void ForceWin(PlayerId id);
    }
}