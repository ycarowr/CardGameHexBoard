using HexCardGame.Localisation;
using TMPro;
using HexCardGame;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiAnimationEndGame : UiAnimation, IFinishGame
    {
        const float DelayToNotify = 1f;
        [SerializeField] LocalizationIds localizedText;
        [SerializeField] PlayerId id;
        TMP_Text Text;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Id == id)
            {
                Text.text = Localisation.Localization.Instance.Get(localizedText);
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