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
        private const string Colon = ": ";
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly LocalizationIds _textId = LocalizationIds.ActionPoint;
        [SerializeField] private SeatType id;
        private TextMeshPro MyText { get; set; }
        private string PreBuiltText { get; set; }

        public void OnAddItem(SeatType seatType, IItem item, int total, int amount)
        {
            if (IsMyEvent(seatType))
                if (item.ItemId == ActionPoint.Id)
                    MyText.text = Build(total.ToString());
        }

        public void OnRemoveItem(SeatType seatType, IItem item, int total, int amount)
        {
            if (IsMyEvent(seatType))
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

        private string Build(string text)
        {
            _stringBuilder.Length = 0;
            _stringBuilder.Append(PreBuiltText);
            _stringBuilder.Append(text);
            return _stringBuilder.ToString();
        }

        private bool IsMyEvent(SeatType seatType)
        {
            return seatType == id;
        }
    }
}