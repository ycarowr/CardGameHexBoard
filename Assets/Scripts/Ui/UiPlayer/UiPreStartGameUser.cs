using System.Collections.Generic;
using Game.Ui;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput))]
    [RequireComponent(typeof(IUiPlayer))]
    public class UiPreStartGameUser : UiEventListener, IPreGameStart
    {
        IUiUserInput UserInput { get; set; }
        IUiPlayer Ui { get; set; }

        void IPreGameStart.OnPreGameStart(List<IPlayer> players)
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