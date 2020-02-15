using Game.Ui;
using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    [Event]
    public interface ISelectBoardPosition
    {
        void OnSelectBoardPosition(Vector3Int position);
    }

    [RequireComponent(typeof(ITileMapInput))]
    public class UiBoardPositionSelector : UiEventListener, IUiInputElement, IOnClickTile
    {
        [SerializeField] UiCardHandSelector cardHandSelector;

        void IOnClickTile.OnClickTile(Vector3Int position)
        {
            if (IsLocked) return;
            Dispatcher.Notify<ISelectBoardPosition>(i => i.OnSelectBoardPosition(position));
        }

        public bool IsLocked { get; private set; }
        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;

        protected override void Awake()
        {
            base.Awake();
            Lock();
            cardHandSelector.OnCardSelected += card => Unlock();
            cardHandSelector.OnCardUnSelect += Lock;
        }
    }
}