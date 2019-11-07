using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using Tools.Extensions.List;
using Tools.UI.Card;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiHand : UiEventListener, ICreateHand, IDrawCard
    {
        Dictionary<CardHand, IUiCard> _cards = new Dictionary<CardHand, IUiCard>();
        [SerializeField] PlayerId id;
        [SerializeField] [Tooltip("Prefab of the Card")]
        GameObject cardPrefab;
        [SerializeField] [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;
        [SerializeField] UiCardHand cardHand;

        public void OnCreateHand(IHand hand, PlayerId id)
        {
            if (this.id == id)
                Logger.Log<UiHand>("Created View Hand for id: " + id);
        }

        void IDrawCard.OnDrawCard(PlayerId id, CardHand card)
        {
            if (this.id == id)
                _cards.Add(card, GetCard());
                
        }

        [Button]
        public IUiCard GetCard()
        {
            var cardGo = Instantiate(cardPrefab, cardHand.transform);
            var uiCard = cardGo.GetComponent<IUiCard>();
            uiCard.transform.position = deckPosition.position;
            cardHand.AddCard(uiCard);
            return uiCard;
        }

        [Button]
        public void PlayCard()
        {
            if (cardHand.Cards.Count > 0)
            {
                var randomCard = cardHand.Cards.RandomItem();
                cardHand.PlayCard(randomCard);
            }
        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab)) GetCard();
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) PlayCard();
        }
    }
}