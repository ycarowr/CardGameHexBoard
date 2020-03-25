using HexCardGame.Localisation;
using HexCardGame.Runtime.Game;
using TMPro;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiAnimationEndGame : UiAnimation, IFinishGame
    {
        private const float DelayToNotify = 1f;
        [SerializeField] private SeatType id;
        [SerializeField] private LocalizationIds localizedText;
        private TMP_Text Text;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Seat == id)
            {
                Text.text = Localization.Instance.Get(localizedText);
                StartCoroutine(Animate(DelayToNotify));
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Text = GetComponent<TMP_Text>();
        }
    }
}