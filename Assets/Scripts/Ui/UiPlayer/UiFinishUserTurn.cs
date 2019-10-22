using Tools.Patterns.GameEvents;
using HexCardGame;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput))]
    [RequireComponent(typeof(IUiPlayer))]
    public class UiFinishUserTurn : UiGameEventListener, IFinishPlayerTurn
    {
        IUiUserInput UserInput { get; set; }
        IUiPlayer Ui { get; set; }

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