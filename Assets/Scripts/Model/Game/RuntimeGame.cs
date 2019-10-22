using System.Collections.Generic;
using HexCardGame.Model.TurnLogic;
using HexCardGame;
using Tools.Patterns.StateMachine;
using UnityEngine;

namespace HexCardGame.Model.Game
{
    /// <summary>  Simple concrete Game Implementation.TODO: Consider to break this class down into small partial classes. </summary>
    public class RuntimeGame : IGame
    {
        //----------------------------------------------------------------------------------------------------------

        #region Constructor

        public RuntimeGame(IGameController controller, List<IPlayer> players, GameParameters gameParameters, EventsDispatcher gameEvents)
        {
            GameParameters = gameParameters;
            GameEvents = gameEvents;
            
            TurnLogic = new TurnMechanics(players);
            
            Fsm = new TurnBasedFsm(controller, this, gameParameters, gameEvents);
            
            ProcessPreStartGame = new PreStartGameMechanics(this);
            ProcessStartGame = new StartGameMechanics(this);
            ProcessStartPlayerTurn = new StartPlayerTurnMechanics(this);
            ProcessFinishPlayerTurn = new FinishPlayerTurnMechanics(this);
            ProcessFinishGame = new FinishGameMechanics(this);

            AddMechanic(ProcessPreStartGame);
            AddMechanic(ProcessStartGame);
            AddMechanic(ProcessStartPlayerTurn);
            AddMechanic(ProcessFinishPlayerTurn);
            AddMechanic(ProcessFinishGame);
        }

        #endregion


        void AddMechanic(BaseGameMechanics mechanic) => Mechanics.Add(mechanic);

        //----------------------------------------------------------------------------------------------------------

        #region Properties
        
        public TurnBasedFsm Fsm { get; set; }

        public List<IPlayer> Players => TurnLogic.Players;
        public bool IsGameStarted { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsTurnInProgress { get; set; }
        public GameParameters GameParameters { get; }
        public EventsDispatcher GameEvents { get; }

        #region Processes

        public List<BaseGameMechanics> Mechanics { get; set; } = new List<BaseGameMechanics>();
        public ITurnLogic TurnLogic { get; set; }
        PreStartGameMechanics ProcessPreStartGame { get; }
        StartGameMechanics ProcessStartGame { get; }
        StartPlayerTurnMechanics ProcessStartPlayerTurn { get; }
        FinishPlayerTurnMechanics ProcessFinishPlayerTurn { get; }
        FinishGameMechanics ProcessFinishGame { get; }

        #endregion

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Execution

        public void PreStartGame() => ProcessPreStartGame.Execute();

        public void StartGame() => ProcessStartGame.Execute();

        public void StartCurrentPlayerTurn() => ProcessStartPlayerTurn.Execute();

        public void FinishCurrentPlayerTurn() => ProcessFinishPlayerTurn.Execute();

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            ProcessFinishGame.Execute(player);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}