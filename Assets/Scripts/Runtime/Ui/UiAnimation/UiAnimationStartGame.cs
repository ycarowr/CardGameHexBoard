using HexCardGame.Runtime.Game;
using TMPro;

namespace HexCardGame.UI
{
    public class UiAnimationStartGame : UiAnimation, IStartGame
    {
        private const float DelayToNotify = 0.75f;
        private TMP_Text Text;

        void IStartGame.OnStartGame(IPlayer player)
        {
            Text.text = player.Seat + " player starts!";
            StartCoroutine(Animate(DelayToNotify));
        }

        protected override void Awake()
        {
            base.Awake();
            Text = GetComponent<TMP_Text>();
        }
    }
}