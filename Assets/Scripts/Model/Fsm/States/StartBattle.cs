using System.Collections;
using HexCardGame.Model.Game;
using UnityEngine;

namespace HexCardGame
{
    public class StartBattle : BaseBattleState, IStartGame
    {
        //----------------------------------------------------------------------------------------------------------

        #region Constructor

        public StartBattle(BattleFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        public override void OnEnterState()
        {
            base.OnEnterState();
            //schedule pre game
            Fsm.Controller.MonoBehaviour.StartCoroutine(PreGameRoutine());

            //schedule start game
            Fsm.Controller.MonoBehaviour.StartCoroutine(StartGameRoutine());
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Game Events

        void IStartGame.OnStartGame(IPlayer starter)
        {
            var nextState = Fsm.GetPlayerState(starter.Id);
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