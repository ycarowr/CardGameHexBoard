using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Dicard/Play cards when the object is clicked.
    /// </summary>
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDiscardClick : MonoBehaviour
    {
        UiCardUtils Utils { get; set; }
        IMouseInput Input { get; set; }

        void Awake()
        {
            Utils = transform.parent.GetComponentInChildren<UiCardUtils>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += PlayRandom;
        }

        void PlayRandom(PointerEventData obj)
        {
        }
    }
}