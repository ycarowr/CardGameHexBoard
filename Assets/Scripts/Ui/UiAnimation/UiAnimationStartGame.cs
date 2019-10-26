using TMPro;

namespace HexCardGame.UI
{
    public class UiAnimationStartGame : UiAnimation, IStartGame
    {
        const float DelayToNotify = 0.75f;
        TMP_Text Text;

        void IStartGame.OnStartGame(IPlayer player)
        {
            Text.text = player.Id + " player starts!";
            StartCoroutine(Animate(DelayToNotify));
        }

        protected override void Awake()
        {
            base.Awake();
            Text = GetComponent<TMP_Text>();
        }
    }
}