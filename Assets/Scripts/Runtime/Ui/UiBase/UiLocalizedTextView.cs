using HexCardGame.Localisation;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiLocalizedTextView : UiText
    {
        [SerializeField] private LocalizationIds id;

        protected override void Awake()
        {
            base.Awake();
            SetText(Localization.Instance.Get(id));
        }
    }
}