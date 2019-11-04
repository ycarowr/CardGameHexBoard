using System.Collections;
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

        [SerializeField] [Tooltip("Prefab of the Card C#")]
        GameObject cardPrefabCs;

        [SerializeField] [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;

        [SerializeField] [Tooltip("Game view transform")]
        Transform gameView;

        UiCardHand CardHand { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        void Awake() => CardHand = transform.parent.GetComponentInChildren<UiCardHand>();

        IEnumerator Start()
        {
            //starting cards
            for (var i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DrawCard();
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        [Button]
        public void DrawCard()
        {
            //TODO: Consider replace Instantiate by an Object Pool Pattern
            var cardGo = Instantiate(cardPrefabCs, gameView);
            cardGo.name = "Card_" + Count;
            var card = cardGo.GetComponent<IUiCard>();
            card.transform.position = deckPosition.position;
            Count++;
            CardHand.AddCard(card);
        }

        [Button]
        public void PlayCard()
        {
            if (CardHand.Cards.Count > 0)
            {
                var randomCard = CardHand.Cards.RandomItem();
                CardHand.PlayCard(randomCard);
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