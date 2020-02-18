namespace HexCardGame.UI
{
    public class UiButtonDraw : UiButton
    {
        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressDraw pressDraw)
                AddListener(pressDraw.Draw);
        }

        public interface IPressDraw : IButtonHandler
        {
            void Draw();
        }
    }
}