using HexCardGame.Localisation;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiLocalizedTextView : UiText
    {
        [SerializeField] LocalizationIds id;

        protected override void Awake()
        {
            base.Awake();
            SetText(Localisation.Localization.Instance.Get(id));
        }
    }
}