using System;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardHandSelector))]
    public class UiCardHandSorter : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        const float OffsetZ = -0.3f;
        IUiCardPile CardHand { get; set; }

        //--------------------------------------------------------------------------------------------------------------

        void Awake()
        {
            CardHand = GetComponent<UiCardHandSelector>();
            CardHand.OnPileChanged += Sort;
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Sort(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var layerZ = 0f;
            foreach (var card in cards)
            {
                var localCardPosition = card.transform.localPosition;
                localCardPosition.z = layerZ;
                card.transform.localPosition = localCardPosition;
                layerZ += OffsetZ;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}