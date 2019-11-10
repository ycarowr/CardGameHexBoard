using Game.Ui;
using HexCardGame.Runtime.GamePool;

namespace HexCardGame.UI
{
    public class UiPoolReturnCardSelector : UiEventListener, ISelectPickPoolPosition
    {
        void ISelectPickPoolPosition.OnSelectPickPoolPosition(PlayerId playerId, PositionId positionId) =>
            GameData.CurrentGameInstance.PickCardFromPosition(PlayerId.User, positionId);
    }
}