using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IUiUserInput))]
    [RequireComponent(typeof(IUiPlayer))]
    public class UiUserHudButtons : MonoBehaviour,
        UiButtonRandom.IPressPassTurn,
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
            var buttons = gameObject.GetComponentsInChildren<UiButton>();
            foreach (var button in buttons)
                button.SetHandler(this);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        void DisableInput() => UserInput.Disable();

        //----------------------------------------------------------------------------------------------------------

        #region Buttons

        void UiButtonRandom.IPressPassTurn.PassTurn()
        {
//            if (Ui.GameData.CurrentGameInstance.TurnLogic.GetPlayer(Ui.Id).PassTurn())
//                DisableInput();
        }

        void UiButtonLose.IPressLose.PressLose()
        {
//            Ui.Lose();
            DisableInput();
        }

        void UiButtonWin.IPressWin.PressWin()
        {
//            Ui. Win();
            DisableInput();
        }

        #endregion
    }
}