using Game.Ui;
using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    [Event]
    public interface ISelectBoardPosition
    {
        void OnSelectPosition(Vector3Int position);
    }

    [RequireComponent(typeof(ITileMapInput))]
    public class UiBoardPositionSelector : UiEventListener, IUiInputElement, IOnClickTile
    {
        [SerializeField] UiCardHand cardHand;
        protected override void Awake()
        {
            base.Awake();
            Lock();
            cardHand.OnCardSelected += (card) => Unlock();
            cardHand.OnCardUnSelect += Lock;
        }

        void IOnClickTile.OnClickTile(Vector3Int position)
        {
            if (IsLocked) return;
            
            Dispatcher.Notify<ISelectBoardPosition>(i => i.OnSelectPosition(position));
        }

        public bool IsLocked { get; private set; }
        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
    }
}