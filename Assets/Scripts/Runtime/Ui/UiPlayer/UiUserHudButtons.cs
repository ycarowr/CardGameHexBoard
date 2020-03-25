using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput)), RequireComponent(typeof(UiUser))]
    public class UiUserHudButtons : MonoBehaviour,
        UiButtonPassTurn.IPressPassTurn,
        UiButtonLose.IPressLose,
        UiButtonWin.IPressWin
    {
        UiUser Ui { get; set; }
        IUiUserInput UserInput { get; set; }

        //----------------------------------------------------------------------------------------------------------

        #region Unity callback 

        void Awake()
        {
            UserInput = GetComponent<IUiUserInput>();
            Ui = GetComponent<UiUser>();
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
            if (Ui.GameData.CurrentGameInstance.BattleFsm.TryPassTurn(Ui.Id))
                DisableInput();
        }

        void UiButtonLose.IPressLose.PressLose()
        {
            Ui.GameData.CurrentGameInstance.ForceWin(SeatType.Top);
            DisableInput();
        }

        void UiButtonWin.IPressWin.PressWin()
        {
            Ui.GameData.CurrentGameInstance.ForceWin(SeatType.Bottom);
            DisableInput();
        }

        #endregion
    }
}