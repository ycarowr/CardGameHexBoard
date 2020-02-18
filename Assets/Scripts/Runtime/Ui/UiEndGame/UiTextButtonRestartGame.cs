using HexCardGame.Localisation;

namespace HexCardGame.UI
{
    public class UiTextButtonRestartGame : UiText
    {
        protected override void Awake()
        {
            base.Awake();
            var restartText = Localization.Instance.Get(LocalizationIds.Restart);
            SetText(restartText);
        }
    }
}