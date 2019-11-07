using HexCardGame.UI;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDrawerClick : MonoBehaviour
    {
        [SerializeField] UiHand uiHand;
        IMouseInput Input { get; set; }

        void Awake()
        {
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += DrawCard;
        }

        void DrawCard(PointerEventData obj) => uiHand.GetCard();
    }
}