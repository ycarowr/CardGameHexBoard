using HexCardGame.Model.Game;
using Tools.Patterns.Observer;
using Tools.Patterns.StateMachine;
using HexCardGame;

namespace HexCardGame
{
    /// <summary>
    ///     The base of all the battle states. States work as controllers to provide access to a funcionality in the
    ///     model.
    /// </summary>
    public abstract class BaseBattleState : IState, IListener, IRestartGame
    {
        //----------------------------------------------------------------------------------------------------------

        #region Constructor

        protected BaseBattleState(TurnBasedFsm fsm, IGame game, GameParameters gameParameters, EventsDispatcher gameEvents)
        {
            Fsm = fsm;
            Game = game;
            GameParameters = gameParameters;
            GameEvents = gameEvents;

            //Subscribe game events 
            GameEvents.AddListener(this);
            IsInitialized = true;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Properties

        protected GameParameters GameParameters { get; }
        protected EventsDispatcher GameEvents { get; }
        protected IGame Game { get; }
        public TurnBasedFsm Fsm { get; set; }
        public bool IsInitialized { get; }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        public virtual void OnClear() => GameEvents.RemoveListener(this);

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