using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput))]
    [RequireComponent(typeof(IUiPlayer))]
    public class UiUserHudButtons : MonoBehaviour,
        UiButtonPassTurn.IPressPassTurn,
        UiButtonLose.IPressLose,
        UiButtonWin.IPressWin
    {
        IUiPlayer Ui { get; set; }
        IUiUserInput UserInput { get; set; }

        //----------------------------------------------------------------------------------------------------------

        #region Unity callback 

        void Awake()
        {
            UserInput = GetComponent<IUiUserInput>();
            Ui = GetComponent<IUiPlayer>();
            var buttons = GetComponentsInChildren<UiButton>();
            foreach (var button in buttons)
                button.SetHandler(this);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        void DisableInput() => UserInput.Disable();

        //----------------------------------------------------------------------------------------------------------

        #region Buttons

        void UiButtonPassTurn.IPressPassTurn.PassTurn()
        {
            if (Ui.GameDataReference.CurrentGameInstance.BattleFsm.TryPassTurn(Ui.Id))
                DisableInput();
        }

        void UiButtonLose.IPressLose.PressLose()
        {
            Ui.GameDataReference.CurrentGameInstance.ForceWin(PlayerId.Enemy);
            DisableInput();
        }

        void UiButtonWin.IPressWin.PressWin()
        {
            Ui.GameDataReference.CurrentGameInstance.ForceWin(PlayerId.User);
            DisableInput();
        }

        #endregion
    }
}