namespace HexCardGame.UI
{
    public class UiTextPassTurn : UiText
    {
        private readonly string PassTurn = "Pass Turn";

        protected override void Awake()
        {
            base.Awake();
            SetText(PassTurn);
        }
    }
}