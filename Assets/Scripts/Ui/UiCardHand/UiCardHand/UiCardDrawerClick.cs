using Game.Ui;
using HexCardGame;
using HexCardGame.UI;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDrawerClick : UiEventListener
    {
        [SerializeField] UiHand uiHand;
        IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += DrawCard;
        }

        void DrawCard(PointerEventData obj) => GameData.CurrentGameInstance.DrawCardFromLibrary(PlayerId.User);
    }
}