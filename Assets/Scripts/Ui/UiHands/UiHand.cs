using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using Tools.Extensions.Arrays;
using Tools.UI.Card;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiHand : UiEventListener, ICreateHand, IDrawCard, ICreateBoardElement
    {
        readonly Dictionary<IUiCard, CardHand> _cards = new Dictionary<IUiCard, CardHand>();
        [SerializeField] UiCardHand cardHand;

        [SerializeField] [Tooltip("Prefab of the Card")]
        GameObject cardPrefab;

        [SerializeField] [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;

        [SerializeField] PlayerId id;

        void ICreateBoardElement.OnCreateBoardElement(PlayerId id, BoardElement boardElement, Vector2Int position,
            CardHand card)
        {
            if (id != this.id) return;
            RemoveCard(card);
        }

        public void OnCreateHand(IHand hand, PlayerId id)
        {
            if (this.id == id)
                Logger.Log<UiHand>("Created View Hand for id: " + id);
        }

        void IDrawCard.OnDrawCard(PlayerId id, CardHand card)
        {
            if (this.id == id)
                _cards.Add(GetCard(), card);
        }

        protected override void Awake()
        {
            base.Awake();
            cardHand.OnCardPlayed += PlayCard;
        }

        [Button]
        public IUiCard GetCard()
        {
            var cardGo = Instantiate(cardPrefab, cardHand.transform);
            var uiCard = cardGo.GetComponent<IUiCard>();
            uiCard.transform.position = deckPosition.position;
            uiCard.gameObject.SetActive(true);
            cardHand.AddCard(uiCard);
            return uiCard;
        }

        void PlayCard(IUiCard uiCard)
        {
            if (uiCard == null)
                return;

            var rdn = GameData.CurrentGameInstance.Board.Positions.RandomItem();
            GameData.CurrentGameInstance.PlayElementAt(id, _cards[uiCard], rdn);
        }

        void RemoveCard(CardHand card)
        {
            IUiCard removed = null;
            foreach (var key in _cards.Keys)
                if (_cards[key] == card)
                    removed = key;

            if (removed != null)
                _cards.Remove(removed);

            Destroy(removed.gameObject);
        }
    }
}