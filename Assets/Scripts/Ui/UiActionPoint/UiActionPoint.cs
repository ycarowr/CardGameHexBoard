using System.Text;
using Game.Ui;
using HexCardGame.Localisation;
using HexCardGame.Runtime;
using TMPro;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiActionPoint : UiEventListener, IAddItem, IRemoveItem
    {
        const string Colon = ": ";
        readonly StringBuilder _stringBuilder = new StringBuilder();
        readonly LocalizationIds _textId = LocalizationIds.ActionPoint;
        [SerializeField] PlayerId id;
        TextMeshPro MyText { get; set; }
        string PreBuiltText { get; set; }

        public void OnAddItem(PlayerId playerId, IItem item, int total, int amount)
        {
            if (IsMyEvent(playerId))
                if (item.ItemId == ActionPoint.Id)
                    MyText.text = Build(total.ToString());
        }

        public void OnRemoveItem(PlayerId playerId, IItem item, int total, int amount)
        {
            if (IsMyEvent(playerId))
                if (item.ItemId == ActionPoint.Id)
                    MyText.text = Build(total.ToString());
        }

        protected override void Awake()
        {
            base.Awake();
            MyText = GetComponentInChildren<TextMeshPro>();
            _stringBuilder.Append(Localization.Instance.Get(_textId));
            _stringBuilder.Append(Colon);
            PreBuiltText = _stringBuilder.ToString();
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