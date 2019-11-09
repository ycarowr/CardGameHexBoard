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
    public class UiBoardPositionSelector : UiEventListener, IOnClickTile
    {
        void IOnClickTile.OnClickTile(Vector3Int position) =>
            Dispatcher.Notify<IOnSelectBoardPosition>(i => i.OnSelectPosition(position));
    }
}