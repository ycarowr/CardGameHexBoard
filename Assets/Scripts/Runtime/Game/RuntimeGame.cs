using System.Collections.Generic;
using HexCardGame.Runtime.GameBoard;
using HexCardGame.Runtime.GamePool;
using HexCardGame.Runtime.GameScore;
using HexCardGame.Runtime.GameTurn;
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
            Parameters = args.GameParameters;
            GameMechanics = args.GameMechanics;
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
            Board = new Board<Creature>(args.GameParameters, Dispatcher);

            //Create Pool
            Pool = new Pool<CardPool>(args.GameParameters, Dispatcher);

            {
                //Create Library
                var libData = new Dictionary<PlayerId, CardData[]>
                {
                    {PlayerId.User, args.GameParameters.PlayerDeck.GetDeck()},
                    {PlayerId.Ai, args.GameParameters.PlayerDeck.GetDeck()}
                };

                Library = new Library(libData, Dispatcher);
            }
        }

        void InitializeTurnBasedStructures(GameArgs args)
        {
            TurnLogic = new TurnLogic(Players);
            BattleFsm = new BattleFsm(args, this);
        }

        public struct GameArgs
        {
            public IDispatcher Dispatcher;
            public IGameController Controller;
            public GameMechanics GameMechanics;
            public GameParameters GameParameters;
        }
    }

    public class GameMechanics
    {
        public HandPool HandPool { get; private set; }
        public HandBoard HandBoard { get; private set; }
        public StartGame StartGame { get; private set; }
        public FinishGame FinishGame { get; private set; }
        public HandLibrary HandLibrary { get; private set; }
        public PoolLibrary PoolLibrary { get; private set; }
        public PreStartGame PreStartGame { get; private set; }
        public StartPlayerTurn StartPlayerTurn { get; private set; }
        public FinishPlayerTurn FinishPlayerTurn { get; private set; }

        public void Initialize(IGame game)
        {
            HandPool = new HandPool(game);
            StartGame = new StartGame(game);
            HandBoard = new HandBoard(game);
            FinishGame = new FinishGame(game);
            PoolLibrary = new PoolLibrary(game);
            HandLibrary = new HandLibrary(game);
            PreStartGame = new PreStartGame(game);
            StartPlayerTurn = new StartPlayerTurn(game);
            FinishPlayerTurn = new FinishPlayerTurn(game);
        }
    }
}