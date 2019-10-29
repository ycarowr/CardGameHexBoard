using Game.Ui;
using HexCardGame.Model.GamePool;

namespace HexCardGame.UI
{
    public class UiPoolDebug : UiEventListener, ICreatePool
    {
        void ICreatePool.OnCreatePool(IPool pool)
        {
        }
    }
}