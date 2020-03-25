using Game.Ui;
using HexCardGame.UI;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDrawerClick : UiEventListener
    {
        [SerializeField] private UiLibraryMenu menu;
        private IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += ShowMenu;
        }

        private void ShowMenu(PointerEventData obj)
        {
            menu.Show();
        }
    }
}