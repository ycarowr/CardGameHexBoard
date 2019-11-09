using Game.Ui;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Base zones where the user can drop a UI Card.
    /// </summary>
    [RequireComponent(typeof(IMouseInput))]
    public abstract class UiBaseDropZone : UiEventListener
    {
        protected UiCardHand CardHand { get; set; }
        protected IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            CardHand = transform.parent.GetComponentInChildren<UiCardHand>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerUp += OnPointerUp;
        }

        protected virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}