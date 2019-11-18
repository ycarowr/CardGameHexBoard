using System.Collections;
using System.Collections.Generic;
using System.Text;
using Game.Ui;
using HexCardGame.Localisation;
using HexCardGame.Runtime;
using TMPro;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiGold : UiEventListener, IAddGold, IRemoveGold
    {
        LocalizationIds _textId = LocalizationIds.Gold;
        StringBuilder _stringBuilder = new StringBuilder();
        const string Colon = ": ";
        [SerializeField] PlayerId id;
        TextMeshPro MyText { get; set; }
        string PreBuiltText { get; set; }
        protected override void Awake()
        {
            base.Awake();
            MyText = GetComponentInChildren<TextMeshPro>();
            _stringBuilder.Append(Localization.Instance.Get(_textId));
            _stringBuilder.Append(Colon);
            PreBuiltText = _stringBuilder.ToString();
        }

        public void OnAddGold(PlayerId playerId, int total, int amount)
        {
            if(IsMyEvent(playerId))
                MyText.text = Build(total.ToString());
        }

        public void OnRemoveGold(PlayerId playerId, int total, int amount)
        {
            if (IsMyEvent(playerId))
                MyText.text = Build(total.ToString());
        }

        string Build(string text)
        {
            _stringBuilder.Length = 0;
            _stringBuilder.Append(PreBuiltText);
            _stringBuilder.Append(text);
            return _stringBuilder.ToString();
        }

        bool IsMyEvent(PlayerId playerId) => playerId == id;
    }
}