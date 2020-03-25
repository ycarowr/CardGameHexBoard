using TMPro;
using UnityEngine.UI;

namespace HexCardGame.UI
{
    public class UiButtonWin : UiButton
    {
        private UITextMeshImage UiButton { get; set; }

        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressWin win)
                AddListener(win.PressWin);
        }

        protected void Awake()
        {
            UiButton = new UITextMeshImage(
                GetComponentInChildren<TMP_Text>(),
                GetComponent<Image>());
        }

        public interface IPressWin
        {
            void PressWin();
        }
    }
}