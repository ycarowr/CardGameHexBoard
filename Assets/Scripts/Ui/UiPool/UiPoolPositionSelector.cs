using System;
using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.Runtime.GamePool;
using Tools.Input.Mouse;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexCardGame.UI
{
    [Event]
    public interface ISelectPoolPosition
    {
        void OnSelectPoolPosition(PlayerId playerId, PositionId positionId);
    }

    [RequireComponent(typeof(IMouseInput))]
    public class UiPoolPositionSelector : UiEventListener, IUiInputElement
    {
        UiPoolPosition Position { get; set; }
        IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Position = GetComponent<UiPoolPosition>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += OnPointerClick;
            Unlock();
        }

        void OnPointerClick(PointerEventData eventData)
        {
            Dispatcher.Notify<ISelectPoolPosition>(i => i.OnSelectPoolPosition(PlayerId.User, Position.Id));
        }

        public bool IsLocked => !Input.IsTracking;
        public void Lock() => Input.StopTracking();
        public void Unlock() => Input.StartTracking();
    }
}
