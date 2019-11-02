using System.Collections.Generic;
using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.GameScore;
using HexCardGame.Runtime.TurnLogic;
using HexCardGame.SharedData;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    /// <summary>  Game Model Implementation. </summary>
    public partial class RuntimeGame : IGame
    {
        public RuntimeGame(GameArgs args)
        {
            Dispatcher = args.Dispatcher;
            InitializeGameDataStructures(args);
            InitializeTurnBasedStructures(args);
        }

        void InitializeGameDataStructures(GameArgs args)
        {
            {
                //Create Players
                var userId = args.GameParameters.Profiles.UserPlayer.id;
                var aiId = args.GameParameters.Profiles.AiPlayer.id;
                var user = new Player(userId, args.GameParameters, args.Dispatcher);
                var ai = new Player(aiId, args.GameParameters, args.Dispatcher);
                Players = new[] {user, ai};

                //Create Hands
                Hands = new[]
                {
                    new Hand(user.Id, args.GameParameters, Dispatcher),
                    new Hand(ai.Id, args.GameParameters, Dispatcher)
                };

                //Create Score
                Score = new Score(Players, args.GameParameters, args.Dispatcher);
            }

            //Create Board
            Board = new Board(args.GameParameters, Dispatcher);

            //Create Pool
            Pool = new Pool(args.GameParameters, Dispatcher);

            {
                //Create Library
                var libData = new Dictionary<PlayerId, CardData[]>
                {
                    {PlayerId.User, new CardData[0]},
                    {PlayerId.Ai, new CardData[0]}
                };

                Library = new Library(libData, Dispatcher);
            }
        }

        void InitializeTurnBasedStructures(GameArgs args)
        {
            TurnLogic = new TurnMechanics(Players);
            BattleFsm = new BattleFsm(args, this);
            PreStartGameMechanics = new PreStartGameMechanics(this);
            StartGameMechanics = new StartGameMechanics(this);
            StartPlayerTurnMechanics = new StartPlayerTurnMechanics(this);
            FinishPlayerTurnMechanics = new FinishPlayerTurnMechanics(this);
            FinishGameMechanics = new FinishGameMechanics(this);
        }

        public struct GameArgs
        {
            public IDispatcher Dispatcher;
            public IGameController Controller;
            public GameParameters GameParameters;
        }
    }
}