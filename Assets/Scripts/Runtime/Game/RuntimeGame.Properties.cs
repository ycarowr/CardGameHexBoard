using System.Collections.Generic;
using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.TurnLogic;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
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
        Dictionary<IPlayer, IHand> Hands { get; set; }

        #endregion

        #region Turn Mechanics

        public ITurnLogic TurnLogic { get; private set; }
        public BattleFsm BattleFsm { get; private set; }
        PreStartGameMechanics PreStartGameMechanics { get; set; }
        StartGameMechanics StartGameMechanics { get; set; }
        StartPlayerTurnMechanics StartPlayerTurnMechanics { get; set; }
        FinishPlayerTurnMechanics FinishPlayerTurnMechanics { get; set; }
        FinishGameMechanics FinishGameMechanics { get; set; }

        #endregion
    }
}