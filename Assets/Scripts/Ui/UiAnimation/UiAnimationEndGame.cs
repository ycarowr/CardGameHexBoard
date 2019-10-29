using HexCardGame.Localisation;
using HexCardGame.Model.Game;
using TMPro;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiAnimationEndGame : UiAnimation, IFinishGame
    {
        const float DelayToNotify = 1f;
        [SerializeField] PlayerId id;
        [SerializeField] LocalizationIds localizedText;
        TMP_Text Text;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Id == id)
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