using System.Collections;
using Game.Ui;
using HexCardGame.Model.Game;
using Tools.Patterns.GameEvents;
using UnityEngine;

namespace HexCardGame.UI
{
    public interface IRestartGameHandler
    {
        void RestartGame();
    }

    /// <summary> End game HUD. Solves model dependencies accessing the game controller via Singleton. </summary>
    [RequireComponent(typeof(IUiUserInput))]
    public class UiEndGameContainer : UiEventListener, IRestartGameHandler, IFinishGame, IStartGame
    {
        const float DelayToEnable = 1f;
        GameDataReference GameDataReference { get; set; }
        IUiUserInput UserInput { get; set; }

        void IFinishGame.OnFinishGame(IPlayer winner) => StartCoroutine(EnableInput());

        void IRestartGameHandler.RestartGame() =>
            GameDataReference.CurrentGameInstance.BattleFsm.Controller.RestartGameImmediately();

        void IStartGame.OnStartGame(IPlayer starter) => UserInput.Disable();

        protected override void Awake()
        {
            base.Awake();
            GameDataReference = GameDataReference.Load();

            //user input
            UserInput = gameObject.AddComponent<UiUserInput>();

            //HUD end game
            gameObject.AddComponent<UiButtonsEndGame>();
        }

        IEnumerator EnableInput()
        {
            yield return new WaitForSeconds(DelayToEnable);
            UserInput.Enable();
        }

        public void RestartGame()
        {
        }
    }
}