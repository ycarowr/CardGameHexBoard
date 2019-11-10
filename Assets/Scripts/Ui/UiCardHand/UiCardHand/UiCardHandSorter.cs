using System;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardHandSelector))]
    public class UiCardHandSorter : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        const int OffsetZ = -1;
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

            var layerZ = 0;
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