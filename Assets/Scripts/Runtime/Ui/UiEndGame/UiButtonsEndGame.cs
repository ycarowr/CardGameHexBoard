using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(IRestartGameHandler))]
    public class UiButtonsEndGame : MonoBehaviour,
        IButtonHandler,
        UiButtonRestart.IPressRestart
    {
        private IRestartGameHandler PlayerHandler { get; set; }

        void UiButtonRestart.IPressRestart.PressRestart()
        {
            PlayerHandler.RestartGame();
        }

        private void Awake()
        {
            PlayerHandler = GetComponent<IRestartGameHandler>();

            var buttons = gameObject.GetComponentsInChildren<UiButton>();
            foreach (var button in buttons)
                button.SetHandler(this);
        }
    }
}