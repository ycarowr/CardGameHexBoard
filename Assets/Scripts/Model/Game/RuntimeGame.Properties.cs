using HexCardGame.Model.GameBoard;
using HexCardGame.Model.GamePool;
using HexCardGame.Model.TurnLogic;
using Tools.Patterns.Observer;

namespace HexCardGame.Model.Game
{
    public partial class RuntimeGame
    {
        #region Properties

        public IDispatcher Dispatcher { get; }
        public bool IsGameStarted { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsTurnInProgress { get; set; }

        #endregion

        #region Game Data Structures

        IBoard Board { get; set; }
        IPool Pool { get; set; }
        ILibrary Library { get; set; }
        IPlayer[] Players { get; set; }

        #endregion

        #region Turn Mechanics

        public ITurnLogic TurnLogic { get; private set; }
        public BattleFsm BattleFsm { get; private set; }
        PreStartGameMechanics ProcessPreStartGame { get; set; }
        StartGameMechanics ProcessStartGame { get; set; }
        StartPlayerTurnMechanics ProcessStartPlayerTurn { get; set; }
        FinishPlayerTurnMechanics ProcessFinishPlayerTurn { get; set; }
        FinishGameMechanics ProcessFinishGame { get; set; }

        #endregion
    }
}