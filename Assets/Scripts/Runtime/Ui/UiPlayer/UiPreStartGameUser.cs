using Game.Ui;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput)), RequireComponent(typeof(IUiPlayer))]
    public class UiPreStartGameUser : UiEventListener, IPreGameStart
    {
        private IUiUserInput UserInput { get; set; }
        private IUiPlayer Ui { get; set; }

        void IPreGameStart.OnPreGameStart(IPlayer[] players)
        {
            if (Ui.IsMyTurn())
                UserInput.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            UserInput = GetComponent<IUiUserInput>();
            Ui = GetComponentInParent<IUiPlayer>();
        }
    }
}