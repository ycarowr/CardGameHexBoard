using HexCardGame.Runtime.Game;
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
        #region Constructor

        protected BaseBattleState(BattleFsm.BattleFsmArguments args)
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

        protected GameParameters GameParameters { get; }
        protected IDispatcher Dispatcher { get; }
        public bool IsInitialized { get; }
        public BattleFsm Fsm { get; }
        protected IGame Game { get; }

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