using System.Collections.Generic;
using HexBoardGame.Runtime;
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
            GameMechanics = new GameMechanics(this);
            InitializeGameDataStructures(args);
            InitializeTurnBasedStructures(args);
        }

        void InitializeGameDataStructures(GameArgs args)
        {
            {
                var localPlayer = args.LocalPlayer;
                var remotePlayer = args.RemotePlayer;
                Players = new[] {localPlayer, remotePlayer};

                //Create Hands
                Hands = new IHand[]
                {
                    new Hand(localPlayer.Seat, args.GameParameters, Dispatcher),
                    new Hand(remotePlayer.Seat, args.GameParameters, Dispatcher)
                };

                //Create Inventories
                Inventories = new IInventory[]
                {
                    new Inventory(localPlayer.Seat, Parameters, Dispatcher),
                    new Inventory(remotePlayer.Seat, Parameters, Dispatcher)
                };

                //Create Score
                Score = new Score(Players, args.GameParameters, args.Dispatcher);
            }

            //Create Board
            Board = new Board<CreatureElement>(args.GameParameters, Dispatcher);

            //Create Board Manipulation
            BoardManipulation = new BoardManipulationOddR(args.GameParameters.BoardData);

            //Create Pool
            Pool = new Pool<CardPool>(args.GameParameters, Dispatcher);

            {
                //Create Library
                var libData = new Dictionary<SeatType, CardData[]>
                {
                    {SeatType.Bottom, args.GameParameters.Profiles.localPlayer.libraryData.GetDeck()},
                    {SeatType.Top, args.GameParameters.Profiles.remotePlayer.libraryData.GetDeck()}
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
            public GameParameters GameParameters;
            public IPlayer LocalPlayer;
            public IPlayer RemotePlayer;
        }
    }

    public struct GameMechanics
    {
        public HandPool HandPool { get; }
        public HandBoard HandBoard { get; }
        public StartGame StartGame { get; }
        public FinishGame FinishGame { get; }
        public HandLibrary HandLibrary { get; }
        public PoolLibrary PoolLibrary { get; }
        public PreStartGame PreStartGame { get; }
        public StartPlayerTurn StartPlayerTurn { get; }
        public FinishPlayerTurn FinishPlayerTurn { get; }

        public GameMechanics(IGame game)
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