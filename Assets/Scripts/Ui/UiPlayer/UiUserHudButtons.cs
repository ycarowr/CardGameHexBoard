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
            if (Ui.GameData.CurrentGameInstance.BattleFsm.TryPassTurn(Ui.Id))
                DisableInput();
        }

        void UiButtonLose.IPressLose.PressLose()
        {
            Ui.GameData.CurrentGameInstance.ForceWin(PlayerId.Ai);
            DisableInput();
        }

        void UiButtonWin.IPressWin.PressWin()
        {
            Ui.GameData.CurrentGameInstance.ForceWin(PlayerId.User);
            DisableInput();
        }

        #endregion
    }
}