namespace HexCardGame.UI
{
    public class UiButtonReveal : UiButton
    {
        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressReveal pressReveal)
                AddListener(pressReveal.Reveal);
        }

        public interface IPressReveal : IButtonHandler
        {
            void Reveal();
        }
    }
}