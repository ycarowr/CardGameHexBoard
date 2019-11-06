using Tools.Extensions.List;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.UI.Card
{
    public class UiCardUtils : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Fields

        int Count { get; set; }

        [SerializeField] [Tooltip("Prefab of the Card")]
        GameObject cardPrefab;

        [SerializeField] [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;

        [SerializeField] UiCardHand cardHand;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        [Button]
        public void DrawCard()
        {
            //TODO: Consider replace Instantiate by an Object Pool Pattern
            var cardGo = Instantiate(cardPrefab, cardHand.transform);
            cardGo.name = "Card_" + Count;
            var card = cardGo.GetComponent<IUiCard>();
            card.transform.position = deckPosition.position;
            Count++;
            cardHand.AddCard(card);
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab)) DrawCard();
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) PlayCard();
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape)) Restart();
        }

        public void Restart() => SceneManager.LoadScene(0);

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}