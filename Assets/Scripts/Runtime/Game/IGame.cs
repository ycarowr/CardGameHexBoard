using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.GameScore;
using HexCardGame.Runtime.GameTurn;
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
        void StartPlayerTurn();
        void FinishPlayerTurn();
    }

    /// <summary> A game interface. </summary>
    public interface IGame : ITurnMechanics, ICardGame
    {
        GameParameters Parameters { get; }
        IDispatcher Dispatcher { get; }
        bool IsGameStarted { get; set; }
        bool IsGameFinished { get; set; }

        IScore Score { get; }
        IHand[] Hands { get; }
        ILibrary Library { get; }
        IPool<CardPool> Pool { get; }
        IBoard<CardBoard> Board { get; }
        void ExecuteAiTurn(PlayerId id);
        void ForceWin(PlayerId id);
    }

    public interface ICardGame
    {
        void DrawCardFromLibrary(IPlayer player);
    }
}