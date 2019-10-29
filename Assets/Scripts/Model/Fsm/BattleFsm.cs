using System.Collections.Generic;
using HexCardGame.Model.Game;
using Tools.Patterns.Observer;
using Tools.Patterns.StateMachine;

namespace HexCardGame
{
    public class BattleFsm : BaseStateMachine
    {
        readonly Dictionary<PlayerId, TurnState> _register = new Dictionary<PlayerId, TurnState>();

        public BattleFsm(RuntimeGame.GameArgs args, IGame game) : base(args.Controller)
        {
            Controller = args.Controller;

            var fsmArgs = new BattleFsmArguments
            {
                Fsm = this,
                Game = game,
                Dispatcher = args.Dispatcher,
                GameParameters = args.GameParameters
            };

            var user = new UserPlayer(fsmArgs);
            var enemy = new EnemyPlayer(fsmArgs);
            var start = new StartBattle(fsmArgs);
            var end = new EndBattle(fsmArgs);

            RegisterState(user);
            RegisterState(enemy);
            RegisterState(start);
            RegisterState(end);

            Initialize();
        }

        public IGameController Controller { get; }

        /// <summary>  Call this method to Push Start Battle State and begin the match. </summary>
        public void StartBattle()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<StartBattle>();
        }

        /// <summary>  Call this method to Push End Battle State and Finish the match. </summary>
        public void EndBattle()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<EndBattle>();
        }

        /// <summary> Call this method to pass the turn of the current player. </summary>
        public bool TryPassTurn(PlayerId id)
        {
            var state = GetPlayerState(id);
            return state.TryPassTurn();
        }

        public void RegisterPlayer(PlayerId id, TurnState state) => _register.Add(id, state);
        public TurnState GetPlayerState(PlayerId id) => _register[id];

        public override void Clear()
        {
            base.Clear();
            _register.Clear();
        }

        public struct BattleFsmArguments
        {
            public IGame Game;
            public BattleFsm Fsm;
            public IDispatcher Dispatcher;
            public GameParameters GameParameters;
        }
    }
}