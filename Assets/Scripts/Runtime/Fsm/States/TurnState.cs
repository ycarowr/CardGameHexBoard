using System.Collections;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame
{
    public abstract class TurnState : BaseBattleState, IFinishPlayerTurn
    {
        protected TurnState(BattleFsm.BattleFsmArguments args) : base(args) => Fsm.RegisterPlayer(Id, this);

        protected bool IsMyTurn => Game.TurnLogic.IsMyTurn(Id);
        protected abstract PlayerId Id { get; }
        protected virtual bool IsAi => false;
        protected virtual bool IsUser => false;
        Coroutine TimeOutRoutine { get; set; }
        Coroutine TickRoutine { get; set; }


        void IFinishPlayerTurn.OnFinishPlayerTurn(IPlayer player)
        {
            if (!IsMyTurn)
                return;

            var nextId = Game.TurnLogic.NextPlayer.Id;
            var nextState = Fsm.GetPlayerState(nextId);
            Fsm.PushState(nextState);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            //setup timer to end the turn
            TimeOutRoutine = Fsm.Controller.MonoBehaviour.StartCoroutine(TimeOut());

            //start current player turn
            Fsm.Controller.MonoBehaviour.StartCoroutine(StartTurn());
        }

        public override void OnExitState()
        {
            base.OnExitState();
            RestartTimeouts();
        }

        /// <summary> Clear the state to the initial configuration and stops all the internal routines. </summary>
        public override void OnClear()
        {
            base.OnClear();
            RestartTimeouts();
        }

        protected virtual void RestartTimeouts()
        {
            if (TimeOutRoutine != null)
                Fsm.Controller.MonoBehaviour.StopCoroutine(TimeOutRoutine);
            TimeOutRoutine = null;

            if (TickRoutine != null)
                Fsm.Controller.MonoBehaviour.StopCoroutine(TickRoutine);
            TickRoutine = null;
        }

        /// <summary> Passes the turn to the next player. </summary>
        public bool TryPassTurn()
        {
            if (!IsMyTurn)
                return false;
            Game.FinishPlayerTurn();
            return true;
        }

        /// <summary> Finishes the player turn. </summary>
        protected virtual IEnumerator TimeOut()
        {
            //disable the timeout for player
            if (IsUser)
                yield return 0;

            if (TimeOutRoutine != null)
            {
                Fsm.Controller.MonoBehaviour.StopCoroutine(TimeOutRoutine);
                TimeOutRoutine = null;
            }
            else
            {
                yield return new WaitForSeconds(GameParameters.Timers.TimeUntilFinishTurn);
            }

            TryPassTurn();
        }

        /// <summary> Starts the player turn. </summary>
        protected virtual IEnumerator StartTurn()
        {
            yield return new WaitForSeconds(GameParameters.Timers.TimeUntilStartTurn);
            Game.StartPlayerTurn();
        }
    }
}