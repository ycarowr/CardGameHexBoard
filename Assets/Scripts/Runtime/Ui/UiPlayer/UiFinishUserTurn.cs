using Game.Ui;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput)), RequireComponent(typeof(IUiPlayer))]
    public class UiFinishUserTurn : UiEventListener, IFinishPlayerTurn
    {
        private IUiUserInput UserInput { get; set; }
        private IUiPlayer Ui { get; set; }

        void IFinishPlayerTurn.OnFinishPlayerTurn(IPlayer player)
        {
            if (Ui.IsUser())
                UserInput.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            Ui = GetComponent<IUiPlayer>();
            UserInput = GetComponent<IUiUserInput>();
        }
    }
}