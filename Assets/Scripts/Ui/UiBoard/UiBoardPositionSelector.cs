using Game.Ui;
using UnityEngine;

namespace HexCardGame.UI
{
    [Event]
    public interface IOnSelectBoardPosition
    {
        void OnSelectPosition(Vector3Int position);
    }

    [RequireComponent(typeof(ITileMapInput))]
    public class UiBoardPositionSelector : UiEventListener, IUiInputElement, IOnClickTile
    {
        protected override void Awake()
        {
            base.Awake();
            Lock();
        }

        void IOnClickTile.OnClickTile(Vector3Int position)
        {
            if (IsLocked) return;
            
            Dispatcher.Notify<IOnSelectBoardPosition>(i => i.OnSelectPosition(position));
        }

        public bool IsLocked { get; private set; }
        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
    }
}