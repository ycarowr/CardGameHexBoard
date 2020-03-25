using System.Collections;
using Game.Ui;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput)), RequireComponent(typeof(IUiPlayer))]
    public class UiStartUserTurn : UiEventListener, IStartPlayerTurn
    {
        private const float DelayToEnableInput = 2;
        private IUiUserInput UserInput { get; set; }
        private IUiPlayer Ui { get; set; }

        //----------------------------------------------------------------------------------------------------------

        #region Game Events

        void IStartPlayerTurn.OnStartPlayerTurn(IPlayer player)
        {
            var isMyTurn = Ui.IsMyTurn();
            var isUser = Ui.IsUser();
            var notAi = !Ui.IsEnemy();

            if (isMyTurn && isUser && notAi)
                StartCoroutine(EnableInput());
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        private IEnumerator EnableInput()
        {
            yield return new WaitForSeconds(DelayToEnableInput);
            UserInput.Enable();
        }

        protected override void Awake()
        {
            base.Awake();
            Ui = GetComponentInParent<IUiPlayer>();
            UserInput = GetComponent<IUiUserInput>();
        }
    }
}