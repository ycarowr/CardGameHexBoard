using HexBoardGame.Runtime;
using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.GameScore;
using HexCardGame.Runtime.GameTurn;
using Tools.Patterns.Observer;
using UnityEngine;

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
        IInventory[] Inventories { get; }
        ILibrary Library { get; }
        IPool<CardPool> Pool { get; }
        IBoard<CreatureElement> Board { get; }
        IBoardManipulation BoardManipulation { get; }
        void ExecuteAiTurn(SeatType id);
        void ForceWin(SeatType id);
    }

    public interface ICardGame
    {
        void RevealCardHigherPosition(SeatType seatType);
        void DrawCardFromLibrary(SeatType seatType);
        void FreeDrawCardFromLibrary(SeatType seatType);
        void PickCardFromPosition(SeatType seatType, PositionId positionId);
        void ReturnCardToPosition(SeatType seatType, CardHand cardHand, PositionId positionId);
        void PlayElementAt(SeatType seatType, CardHand card, Vector3Int position);
    }
}