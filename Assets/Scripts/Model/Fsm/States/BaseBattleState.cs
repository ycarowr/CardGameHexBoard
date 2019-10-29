using HexCardGame.Model.Game;
using Tools.Patterns.Observer;
using Tools.Patterns.StateMachine;

namespace HexCardGame
{
    /// <summary>
    ///     The base of all the battle states. States work as controllers to provide access to a funcionality in the
    ///     model.
    /// </summary>
    public abstract class BaseBattleState : IState, IListener, IRestartGame
    {
        public struct BattleStateArguments
        {
            public IGame Game;
            public BattleFsm Fsm;
            public IDispatcher Dispatcher;
            public GameParametersReference GameParameters;
        }
        
        #region Constructor

        protected BaseBattleState(BattleStateArguments args)
        {
            Fsm = args.Fsm;
            Game = args.Game;
            GameParameters = args.GameParameters;
            Dispatcher = args.Dispatcher;
            Dispatcher.AddListener(this);
            IsInitialized = true;
        }

        ~BaseBattleState() => Dispatcher.RemoveListener(this);

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Properties

        protected GameParametersReference GameParameters { get; }
        protected IDispatcher Dispatcher { get; }
        protected IGame Game { get; }
        public BattleFsm Fsm { get; }
        public bool IsInitialized { get; }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        public virtual void OnClear() => Dispatcher.RemoveListener(this);

        public virtual void OnInitialize()
        {
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnNextState(IState next)
        {
        }

        protected virtual void OnNextState(BaseBattleState nextState)
        {
            Fsm.PopState();
            Fsm.PushState(nextState);
        }

        void IRestartGame.OnRestart() => OnClear();

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}