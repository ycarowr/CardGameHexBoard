using System.Collections;
using Game.Ui;
using Tools.Patterns.GameEvents;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput))]
    [RequireComponent(typeof(IUiPlayer))]
    public class UiStartUserTurn : UiEventListener, IStartPlayerTurn
    {
        const float DelayToEnableInput = 2;
        IUiUserInput UserInput { get; set; }
        IUiPlayer Ui { get; set; }

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

        IEnumerator EnableInput()
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