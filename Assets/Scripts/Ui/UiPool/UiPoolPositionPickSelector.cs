using Game.Ui;
using HexCardGame.Runtime.GamePool;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexCardGame.UI
{
    [Event]
    public interface ISelectPickPoolPosition
    {
        void OnSelectPickPoolPosition(PlayerId playerId, PositionId positionId);
    }

    [RequireComponent(typeof(IMouseInput))]
    public class UiPoolPositionPickSelector : UiEventListener, IUiInputElement
    {
        UiPoolPosition Position { get; set; }
        IMouseInput Input { get; set; }
        public bool IsLocked { get; private set; }

        public void Lock()
        {
            if (IsLocked)
                return;
            IsLocked = true;
            Input.OnPointerClick -= OnPointerClick;
        }

        public void Unlock()
        {
            if (!IsLocked)
                return;
            IsLocked = false;
            Input.OnPointerClick += OnPointerClick;
        }

        protected override void Awake()
        {
            base.Awake();
            Position = GetComponent<UiPoolPosition>();
            Input = GetComponent<IMouseInput>();
            Input.StartTracking();
            IsLocked = false;
            Input.OnPointerClick += OnPointerClick;
        }

        void OnPointerClick(PointerEventData eventData)
        {
            if (IsLocked)
                return;

            if (!Position.HasData)
                return;
            Dispatcher.Notify<ISelectPickPoolPosition>(i => i.OnSelectPickPoolPosition(PlayerId.User, Position.Id));
        }
    }
}