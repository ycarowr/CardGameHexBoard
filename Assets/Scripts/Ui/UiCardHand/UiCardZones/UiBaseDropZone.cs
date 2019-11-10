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
        protected UiCardHandSelector CardHandSelector { get; set; }
        protected IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            CardHandSelector = transform.parent.GetComponentInChildren<UiCardHandSelector>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerUp += OnPointerUp;
        }

        protected virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}