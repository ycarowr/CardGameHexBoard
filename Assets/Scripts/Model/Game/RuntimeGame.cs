using System.Collections.Generic;
using HexCardGame.Model.GameBoard;
using HexCardGame.Model.GamePool;
using HexCardGame.Model.TurnLogic;
using Tools.Patterns.Observer;

namespace HexCardGame.Model.Game
{
    /// <summary>  Game Model Implementation. </summary>
    public partial class RuntimeGame : IGame
    {
        public struct RuntimeGameArgs
        {
            public IGameController Controller;
            public GameParametersReference GameParameters;
            public IDispatcher Dispatcher;
        }

        public RuntimeGame(RuntimeGameArgs args)
        {
            Dispatcher = args.Dispatcher;
            InitializeGameDataStructures(args);
            InitializeTurnBasedStructures(args);
        }

        void InitializeGameDataStructures(RuntimeGameArgs args)
        {
            {   
                //Create and connect players to their seats
                var userId = args.GameParameters.Profiles.UserPlayer.id;
                var aiId = args.GameParameters.Profiles.AiPlayer.id;
                var user = new Player(userId, args.GameParameters, args.Dispatcher);
                var ai = new Player(aiId, args.GameParameters, args.Dispatcher);
                Players = new[] {user, ai};
            }

            //Create Board
            Board = new Board(args.GameParameters, Dispatcher);
            
            //Create Pool
            Pool = new Pool(args.GameParameters, Dispatcher);
            
            {
                //Create Library
                var libData = new Dictionary<PlayerId, List<object>>
                {
                    {PlayerId.User, new List<object>()},
                    {PlayerId.Enemy, new List<object>()}
                };

                Library = new Library(libData, Dispatcher);
            }
        }

        void InitializeTurnBasedStructures(RuntimeGameArgs args)
        {
            TurnLogic = new TurnMechanics(Players);
            BattleFsm = new BattleFsm(args.Controller, this, args.GameParameters, args.Dispatcher);
            ProcessPreStartGame = new PreStartGameMechanics(this);
            ProcessStartGame = new StartGameMechanics(this);
            ProcessStartPlayerTurn = new StartPlayerTurnMechanics(this);
            ProcessFinishPlayerTurn = new FinishPlayerTurnMechanics(this);
            ProcessFinishGame = new FinishGameMechanics(this);
        }
    }
}