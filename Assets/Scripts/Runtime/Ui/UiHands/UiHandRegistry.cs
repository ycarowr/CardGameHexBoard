using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;
using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiCardHandSelector))]
    public class UiHandRegistry : UiEventListener
    {
        private readonly Dictionary<IUiCard, CardHand> _registry = new Dictionary<IUiCard, CardHand>();
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private SeatType id;
        [SerializeField] private Transform libraryPosition;
        [SerializeField] private UiPool uiPool;
        private CardHand SelectedCard { get; set; }
        private ObjectPooler Pooler => ObjectPooler.Instance;
        private UiCardHandSelector CardHandSelector { get; set; }
        public SeatType Id => id;

        protected override void Awake()
        {
            base.Awake();
            CardHandSelector = GetComponent<UiCardHandSelector>();
            CardHandSelector.OnCardSelected += SelectCard;
            CardHandSelector.OnCardUnSelect += Unselect;
        }

        private void OnDestroy()
        {
            CardHandSelector.OnCardSelected -= SelectCard;
            CardHandSelector.OnCardUnSelect -= Unselect;
        }

        public void CreateCardFromLibrary(CardHand cardHand)
        {
            CreateUiCard(cardHand, libraryPosition.position);
        }

        public void CreateCardFromPool(CardHand cardHand, PositionId positionId)
        {
            var poolPosition = uiPool.GetPosition(positionId);
            var poolWorldPosition = poolPosition.transform.position;
            CreateUiCard(cardHand, poolWorldPosition);
        }

        public void RemoveCard(CardHand cardHand)
        {
            CardHandSelector.PlaySelected();
            RemoveUiCard(cardHand);
        }

        public void Clear()
        {
            _registry.Clear();
            SelectedCard = null;
        }

        private void CreateUiCard(CardHand card, Vector3 position)
        {
            var uiCard = Pooler.Get<IUiCard>(cardPrefab);
            var cardTransform = uiCard.transform;
            cardTransform.SetParent(CardHandSelector.transform);
            cardTransform.position = position;
            uiCard.SetAndUpdateView(card.Data, id, this);
            uiCard.Initialize();
            CardHandSelector.AddCard(uiCard);
            _registry.Add(uiCard, card);
        }

        public void RemoveUiCard(CardHand card)
        {
            IUiCard removed = null;
            foreach (var key in _registry.Keys)
                if (_registry[key] == card)
                    removed = key;

            if (removed != null)
                _registry.Remove(removed);

            Pooler.Release(removed?.gameObject);
        }

        public void SelectBoardPosition(Vector3Int position)
        {
            if (SelectedCard == null)
                return;

            GameData.CurrentGameInstance.PlayElementAt(id, SelectedCard, position);
            CardHandSelector.Unselect();
            SelectedCard = null;
        }

        public void ReturnCardToPosition(SeatType id, PositionId positionId)
        {
            if (SelectedCard == null)
                return;

            GameData.CurrentGameInstance.ReturnCardToPosition(this.id, SelectedCard, positionId);
            SelectedCard = null;
        }

        private void SelectCard(IUiCard uiCard)
        {
            SelectedCard = _registry[uiCard];
        }

        private void Unselect()
        {
            SelectedCard = null;
        }
    }
}