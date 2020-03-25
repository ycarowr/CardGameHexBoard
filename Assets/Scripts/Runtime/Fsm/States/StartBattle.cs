using System.Collections;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame
{
    public class StartBattle : BaseBattleState, IStartGame
    {
        //----------------------------------------------------------------------------------------------------------

        #region Constructor

        public StartBattle(BattleFsm.BattleFsmArguments args) : base(args)
        {
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        public override void OnEnterState()
        {
            base.OnEnterState();

            //schedule pre game
            if (Application.isPlaying)
                Fsm.Controller.MonoBehaviour.StartCoroutine(PreGameRoutine());
            else
                Game.PreStartGame();

            //schedule start game
            if (Application.isPlaying)
                Fsm.Controller.MonoBehaviour.StartCoroutine(StartGameRoutine());
            else
                Game.StartGame();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Game Events

        void IStartGame.OnStartGame(IPlayer starter)
        {
            var nextState = Fsm.GetPlayerState(starter.Seat);
            Fsm.Controller.MonoBehaviour.StartCoroutine(NextStateRoutine(nextState));
        }

        IEnumerator NextStateRoutine(BaseBattleState nextState)
        {
            yield return new WaitForSeconds(GameParameters.Timers.TimeUntilFirstPlayer);
            OnNextState(nextState);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Coroutines

        IEnumerator PreGameRoutine()
        {
            yield return new WaitForSeconds(GameParameters.Timers.TimeUntilPreGameEvent);
            Game.PreStartGame();
        }

        IEnumerator StartGameRoutine()
        {
            var preGame = GameParameters.Timers.TimeUntilPreGameEvent;
            var startGame = GameParameters.Timers.TimeUntilStartGameEvent;
            var time = preGame + startGame;
            yield return new WaitForSeconds(time);
            Game.StartGame();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}