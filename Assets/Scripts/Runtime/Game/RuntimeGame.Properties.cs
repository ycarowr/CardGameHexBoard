using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.GameScore;
using HexCardGame.Runtime.GameTurn;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Properties

        public GameParameters Parameters { get; private set; }
        public IDispatcher Dispatcher { get; }
        public bool IsGameStarted { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsTurnInProgress { get; set; }

        #endregion

        #region Game Data Structures

        public IBoard<CardBoard> Board { get; private set; }
        public IPool<CardPool> Pool { get; private set; }
        public ILibrary Library { get; private set; }
        public IPlayer[] Players { get; private set; }
        public IHand[] Hands { get; private  set; }
        public IScore Score { get; private set; }

        #endregion

        #region Turn Mechanics

        public ITurnLogic TurnLogic { get; private set; }
        public BattleFsm BattleFsm { get; private set; }
        PreStartGame _preStartGame;
        StartGame _startGame;
        StartPlayerTurn _startPlayerTurn;
        FinishPlayerTurn _finishPlayerTurn;
        FinishGame _finishGame;
        HandLibrary _handLibrary;

        #endregion
    }
}