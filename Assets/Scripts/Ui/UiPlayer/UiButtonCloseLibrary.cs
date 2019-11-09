namespace HexCardGame.UI
{
    public class UiButtonCloseLibrary : UiButton
    {
        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressCloseLibrary pressCloseLibrary) AddListener(pressCloseLibrary.CloseLibrary);
        }

        public interface IPressCloseLibrary : IButtonHandler
        {
            void CloseLibrary();
        }
    }
}