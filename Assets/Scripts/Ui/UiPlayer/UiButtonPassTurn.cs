namespace HexCardGame.UI
{
    public class UiButtonPassTurn : UiButton
    {
        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressPassTurn pressRandom)
                AddListener(pressRandom.PassTurn);
        }

        public interface IPressPassTurn : IButtonHandler
        {
            void PassTurn();
        }
    }
}